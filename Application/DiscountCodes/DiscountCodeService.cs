using AutoMapper;
using Data.EF;
using Data.Entities;
using Hangfire;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Models.DiscountCodes;
using Models.Dishes;
using Models.Orders;
using Utilities.Constants;

namespace Application.DiscountCodes
{
    public class DiscountCodeService : IDiscountCodeService
    {
        private readonly UOrderDbContext _context;
        private readonly IMapper _mapper;

        public DiscountCodeService(UOrderDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public async Task<List<DiscountCodeVm>> GetAll()
        {
            var results = await _context.DiscountCodes.Where(x => x.IsDeleted == false).Include(one => one.ApplicableProductIds).Select(p => new DiscountCodeVm
            {
                Key = p.Id,
                Id = p.Id,
                CreatedAt = p.CreatedAt,
                Discount = p.Discount,
                Code = p.Code,
                Percentage = p.Percentage,
                MaxDiscountAmount = p.MaxDiscountAmount,
                MinDiscountAmount = p.MinDiscountAmount,
                MinOrderAmountRequired = p.MinOrderAmountRequired,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                AppliesToAllProducts = p.AppliesToAllProducts,
                IsActive = p.IsActive,
                ApplicableProductIds = p.ApplicableProductIds != null ? p.ApplicableProductIds.ToList().Select(i => new DishVm
                {
                    Key = i.Dish.Id,
                    Id = i.Dish.Id,
                    Name = i.Dish.Name,
                    Desc = i.Dish.Desc,
                    Price = i.Dish.Price,
                    IsActive = i.Dish.IsActive,
                    Type = i.Dish.Type,
                    CreatedAt = i.Dish.CreatedAt,
                    TypeName = i.Dish.Type.ToString(),
                    CoverLink = i.Dish.Cover,
                }).ToList() : null,
            }).ToListAsync();
            return results;
        }

        public async Task<int> Create(DiscountCodeCreateRequest req)
        {
            var id = req.Id;
            var item = new DiscountCode
            {
                Id = id,
                Code = req.Code,
                Discount = req.Discount,
                Percentage = req.Percentage,
                MaxDiscountAmount = req.MaxDiscountAmount,
                MinDiscountAmount = req.MinDiscountAmount,
                MinOrderAmountRequired = req.MinOrderAmountRequired,
                StartDate = req.StartDate,
                EndDate = req.EndDate,
                AppliesToAllProducts = req.AppliesToAllProducts,
                IsActive = req.IsActive,
                CreatedAt = req.CreatedAt,
            };
            _context.Add(item);

            if (req.ApplicableProductIds != null)
            {
                foreach (var child in req.ApplicableProductIds)
                {
                    var connect = new DiscountProduct { DishId = child, DiscountCodeId = id };
                    _context.DiscountProducts.Add(connect);
                }
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(string id)
        {
            var itemToDelete = await _context.DiscountCodes.FindAsync(id);

            if (itemToDelete != null)
            {
                itemToDelete.IsDeleted = true;
                BackgroundJob.Schedule(() => HardDelete(itemToDelete.Id), TimeSpan.FromSeconds(SystemConstants.ScheduledDeletionTime));
            }

            await _context.SaveChangesAsync();
            return SystemConstants.ScheduledDeletionTime;
        }

        public async Task HardDelete(string itemId)
        {
            var itemToHardDelete = await _context.DiscountCodes.FindAsync(itemId);

            if (itemToHardDelete != null && itemToHardDelete.IsDeleted)
            {
                _context.DiscountCodes.Remove(itemToHardDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> UndoDelete(string itemId)
        {
            var itemToUndo = await _context.DiscountCodes.FindAsync(itemId);

            if (itemToUndo != null)
            {
                itemToUndo.IsDeleted = false;
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<List<DiscountCodeVm>> ReturnAvailableCodes(CartItemVm vm)
        {
            var availableList = await GetAvailableCodesForUse();

            foreach (var code in availableList)
            {
                code.IsAvailableForUse = ReturnIsAvailableForUse(vm, code);
                code.Discount = CalcutorDiscount(vm, code, code.IsAvailableForUse);
            }
            //var discountCodes = await _context.DiscountCodes.Where(x => x.IsActive == true && DateTime.Today < x.EndDate && x.StartDate < DateTime.Today).Include(x => x.ApplicableProductIds).ThenInclude(x => x.Dish).Select(async x => new DiscountCodeVm
            //{
            //    Key = x.Id,
            //    Id = x.Id,
            //    Code = x.Code,
            //    Percentage = x.Percentage,
            //    MaxDiscountAmount = x.MaxDiscountAmount,
            //    MinDiscountAmount = x.MinDiscountAmount,
            //    MinOrderAmountRequired = x.MinOrderAmountRequired,
            //    StartDate = x.StartDate,
            //    EndDate = x.EndDate,
            //    AppliesToAllProducts = x.AppliesToAllProducts,
            //    IsActive = x.IsActive,
            //    CreatedAt = x.CreatedAt,
            //    ApplicableProductIds = x.ApplicableProductIds != null ? x.ApplicableProductIds.ToList().Select(i => new DishVm
            //    {
            //        Id = i.Dish.Id,
            //    }).ToList() : null,
            //    IsAvailableForUse = ReturnIsAvailableForUse(vm, x.AppliesToAllProducts, x.ApplicableProductIds != null ? availableList, x.MinOrderAmountRequired),
            //    Discount = CalcutorDiscount(vm, x.Discount, x.Percentage, x.MaxDiscountAmount, x.MinDiscountAmount, ReturnIsAvailableForUse(vm, x.AppliesToAllProducts, availableList, x.MinOrderAmountRequired)),
            //}).ToListAsync();

            return availableList.OrderByDescending(x => x.Discount).ToList();
        }

        private async Task<List<DiscountCodeVm>> GetAvailableCodesForUse()
        {
            return await _context.DiscountCodes.Where(x => x.IsActive == true && DateTime.Today < x.EndDate && x.StartDate < DateTime.Today).Include(x => x.ApplicableProductIds).ThenInclude(x => x.Dish).Select(x => new DiscountCodeVm
            {
                Key = x.Id,
                Id = x.Id,
                Code = x.Code,
                Percentage = x.Percentage,
                Discount = x.Discount,
                MaxDiscountAmount = x.MaxDiscountAmount,
                MinDiscountAmount = x.MinDiscountAmount,
                MinOrderAmountRequired = x.MinOrderAmountRequired,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                AppliesToAllProducts = x.AppliesToAllProducts,
                IsActive = x.IsActive,
                CreatedAt = x.CreatedAt,
                ApplicableProductIds = x.ApplicableProductIds != null ? x.ApplicableProductIds.ToList().Select(i => new DishVm
                {
                    Id = i.Dish.Id,
                    Name = i.Dish.Name,
                }).ToList() : null,
            }).ToListAsync();
        }

        private static bool ReturnIsAvailableForUse(CartItemVm vm, DiscountCodeVm code)
        {
            if (CheckAppliesToAllProducts(code, vm) == true && CheckMinOrderAmountRequired(code.MinOrderAmountRequired, vm) == true)
            {
                return true;
            }

            return false;
        }

        private static bool CheckMinOrderAmountRequired(int minOrderAmountRequired, CartItemVm vm)
        {
            if (vm.SubTotal < minOrderAmountRequired)
                return false;
            return true;
        }

        private static bool CheckAppliesToAllProducts(DiscountCodeVm code, CartItemVm vm)
        {
            if (code.AppliesToAllProducts == false)
            {
                foreach (var dish in code.ApplicableProductIds)
                {
                    bool containsCommonElement = vm.Dishes.Any(x => x.DishId.Contains(dish.Id));
                    if (containsCommonElement == true)
                        return true;
                    return false;
                }
            }
            return true;
        }

        private static int CalcutorDiscount(CartItemVm vm, DiscountCodeVm code, bool availableForUse)
        {
            if (availableForUse == false)
            {
                return 0;
            }
            if (code.Discount > 0)
            {
                return code.Discount;
            }

            int maxDiscountValue = vm.SubTotal * code.Percentage / 100;
            if (maxDiscountValue > code.MaxDiscountAmount)
            {
                return code.MaxDiscountAmount;
            }
            if (maxDiscountValue < code.MinDiscountAmount)
            {
                return code.MinDiscountAmount;
            }
            return maxDiscountValue;
        }

        public async Task<int> ApplyDiscountCode(OrderCreateRequest req)
        { return 0; }

        public async Task<int> UpdatePatch(string id, JsonPatchDocument<DiscountCode> patchDoc)
        {
            var stockItem = await GetByIdForUpdateStatus(id);
            var userDto = _mapper.Map<DiscountCode>(stockItem);
            patchDoc.ApplyTo(userDto);
            _context.Update(userDto);

            return await _context.SaveChangesAsync();
        }

        public async Task<DiscountCodeVm> GetById(string id)
        {
            var target = await _context.DiscountCodes.Select(p => new DiscountCodeVm
            {
                Key = p.Id,
                Id = p.Id,
                CreatedAt = p.CreatedAt,
                Discount = p.Discount,
                Code = p.Code,
                Percentage = p.Percentage,
                MaxDiscountAmount = p.MaxDiscountAmount,
                MinDiscountAmount = p.MinDiscountAmount,
                MinOrderAmountRequired = p.MinOrderAmountRequired,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                AppliesToAllProducts = p.AppliesToAllProducts,
                IsActive = p.IsActive,
                ApplicableProductIds = p.ApplicableProductIds != null ? p.ApplicableProductIds.ToList().Select(i => new DishVm
                {
                    Key = i.Dish.Id,
                    Id = i.Dish.Id,
                    Name = i.Dish.Name,
                    Desc = i.Dish.Desc,
                    Price = i.Dish.Price,
                    IsActive = i.Dish.IsActive,
                    Type = i.Dish.Type,
                    CreatedAt = i.Dish.CreatedAt,
                    TypeName = i.Dish.Type.ToString(),
                    CoverLink = i.Dish.Cover,
                }).ToList() : null,
            }).Where(package => package.Id == id).FirstOrDefaultAsync();

            return target;
        }

        public async Task<DiscountCodeVm> GetByIdForUpdateStatus(string id)
        {
            var target = await _context.DiscountCodes.Select(p => new DiscountCodeVm
            {
                Key = p.Id,
                Id = p.Id,
                CreatedAt = p.CreatedAt,
                Discount = p.Discount,
                Code = p.Code,
                Percentage = p.Percentage,
                MaxDiscountAmount = p.MaxDiscountAmount,
                MinDiscountAmount = p.MinDiscountAmount,
                MinOrderAmountRequired = p.MinOrderAmountRequired,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                AppliesToAllProducts = p.AppliesToAllProducts,
                IsActive = p.IsActive,
            }).Where(package => package.Id == id).FirstOrDefaultAsync();

            return target;
        }
    }
}