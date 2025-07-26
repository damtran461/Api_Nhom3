FAS.API - Nhánh 20230915-MasterData

-- 2023/09/15 - BaoNN - FAS-114 - [BE] API Get list/search Department FIN Portal - 20230915-MasterData
(+) Postgres fin_sprint : fns.department_read_by_id
(+) Postgres fin_sprint : fns.department_search

-- 2023/09/15 - BaoNN - FAS-166 : [BE] APi search Asset Type FIN Portal - 20230915-MasterData
(+) Postgres fin_sprint : fns.asset_type_read_by_id
(+) Postgres fin_sprint : fns.asset_type_search

-- 2023/09/15 - BaoNN - FAS-187 : [BE] API Quản lý Customer Posting Group - 20230915-MasterData
(+) Postgres fin_sprint : fns.customer_posting_group_read_all
(+) Postgres fin_sprint : fns.customer_posting_group_read_by_id
(+) Postgres fin_sprint : fns.customer_posting_group_search

-- 2023/09/15 - BaoNN - FAS-193 : [BE] API quản lý Expense Category - 20230915-MasterData
(+) Postgres fin_sprint : fns.expense_category_read_by_id
(+) Postgres fin_sprint : fns.expense_category_search

-- 2023/09/15 - BaoNN -  FAS-199 : [BE] API quản lý Item Posting Group - 20230915-MasterData
(+) Postgres fin_sprint : fns.item_posting_group_read_all
(+) Postgres fin_sprint : fns.item_posting_group_read_by_id
(+) Postgres fin_sprint : fns.item_posting_group_search

-- 2023/09/15 - BaoNN - FAS-174 : [BE] API get thông tin của Subsidiary - 20230915-MasterData
(+) Postgres fin_sprint : fns.subsidiary_read_all
(+) Postgres fin_sprint : fns.subsidiary_read_by_id

-- 2023/09/15 - BaoNN - FAS-106 : Xây dựng bộ api quản lý tax Schedule - 20230915-MasterData
(+) Postgres fin_sprint : fns.tax_schedule_read_all
(+) Postgres fin_sprint : fns.tax_schedule_read_by_id

-- 2023/09/15 - BaoNN - FAS-238 : [BE] APi get list Vendor Posting Group - 20230915-MasterData
(+) Postgres fin_sprint : fns.vendor_posting_group_read_all
(+) Postgres fin_sprint : fns.vendor_posting_group_read_by_id

-- 2023/09/20 - BaoNN - FAS-209 - [BE] Thêm API xem danh sách thông tin Customer Master - 20230915-MasterData
(+) Postgres fin_sprint : fns.customer_master_read_all
(+) Postgres fin_sprint : fns.customer_master_read_by_id
(+) Postgres fin_sprint : fns.customer_master_search
(+) Postgres fin_sprint : fns.customer_master_update
(+) Postgres fin_sprint : fns.customer_master_import_update

-- 2023/09/20 - BaoNN - 20230915-MasterData
(+) Postgres fin_sprint : fns.payment_term_read_all
(+) Postgres fin_sprint : fns.country_read_all
(+) Postgres fin_sprint : mat.currency_unit_read_all

-- 2023/09/29 - BaoNN - FIN-181 : [BE] API quản lý Class (Category) - 20230915-MasterData
(+) Postgres fin_sprint : fns.class_read_all
(+) Postgres fin_sprint : fns.class_read_by_id
(+) Postgres fin_sprint : fns.class_search
(+) Postgres fin_sprint : fns.class_create
(+) Postgres fin_sprint : fns.class_update
(+) Postgres fin_sprint : fns.class_delete

-- 2023/09/29 - BaoNN - FIN-688 :  API mapping Class với Item posting group trên FIN Portal - 20230915-MasterData
(+) Postgres fin_sprint : fns.class_item_posting_group_read_by_id
(+) Postgres fin_sprint : fns.class_item_posting_group_search
(+) Postgres fin_sprint : fns.class_item_posting_group_create
(+) Postgres fin_sprint : fns.class_item_posting_group_create_validate
(+) Postgres fin_sprint : fns.class_item_posting_group_delete
(+) Postgres fin_sprint : fns.class_item_posting_group_update
(+) Postgres fin_sprint : fns.class_item_posting_group_update_validate
(+) Postgres fin_sprint : fns.class_item_posting_group_import_validate
(+) Postgres fin_sprint : fns.class_item_posting_group_import_delete
(+) Postgres fin_sprint : fns.class_item_posting_group_read_4_class
(+) Postgres fin_sprint : fns.class_item_posting_group_read_all

-- 2023/10/09 - BaoNN - FIN-137 : Quản lý Brand - 20230915-MasterData
(+) Postgres fin_sprint : fns.brand_read_all
(+) Postgres fin_sprint : fns.brand_create
(+) Postgres fin_sprint : fns.brand_create_validate
(+) Postgres fin_sprint : fns.brand_update
(+) Postgres fin_sprint : fns.brand_update_validate
(+) Postgres fin_sprint : fns.brand_import_create
(+) Postgres fin_sprint : fns.brand_import_validate

-- 2023/10/11 - BaoNN - FIN-608 : Thêm bộ api quản lý bảng config cho FIN - 20230915-MasterData
(+) Postgres fin_sprint : fin.config_read_all
(+) Postgres fin_sprint : fin.config_read_by_id
(+) Postgres fin_sprint : fin.config_update
(+) Postgres fin_sprint : fin.config_delete
(+) Postgres fin_sprint : fin.config_create
(+) Postgres fin_sprint : fin.config_create_validate
(+) Postgres fin_sprint : fin.config_update_validate

-- 2023/10/11 - BaoNN - FIN-428 : Backsales Group - 20230915-MasterData
(+) Postgres fin_sprint : fns.back_sale_group_read_by_id
(+) Postgres fin_sprint : fns.back_sale_group_read_all
(+) Postgres fin_sprint : fns.back_sale_group_search
(+) Postgres fin_sprint : fns.back_sale_group_create
(+) Postgres fin_sprint : fns.back_sale_group_create_validate
(+) Postgres fin_sprint : fns.back_sale_group_update
(+) Postgres fin_sprint : fns.back_sale_group_update_validate
(+) Postgres fin_sprint : fns.back_sale_group_import_create
(+) Postgres fin_sprint : fns.back_sale_group_import_create_validate

-- 2023/10/11 - BaoNN - Inventory Item - 20230915-MasterData
(+) Postgres fin_sprint : fns.inventory_item_read_all
(+) Postgres fin_sprint : fns.inventory_item_read_by_name

-- 2023/10/12 - BaoNN - Currency - 20230915-MasterData
(+) Postgres fin_sprint : fns.currency_read_all

-- 2023/10/17 - BaoNN - FIN-697 : [BE] Thêm API mapping product_input_type với Item posting group - 20230915-MasterData
(+) Postgres fin_sprint : fns.item_type_posting_group_read_by_id
(+) Postgres fin_sprint : fns.item_type_posting_group_search
(+) Postgres fin_sprint : fns.item_type_posting_group_create
(+) Postgres fin_sprint : fns.item_type_posting_group_update
(+) Postgres fin_sprint : fns.item_type_posting_group_delete
(+) Postgres fin_sprint : fns.item_type_posting_group_create_validate
(+) Postgres fin_sprint : fns.item_type_posting_group_update_validate

-- 2023/10/20 - BaoNN - FIN-751 : [BE] Nâng cấp API mapping Backsale group vs item(Import) - 20230915-MasterData
(+) Postgres fin_sprint : fns.back_sale_group_inventory_item_read_all
(+) Postgres fin_sprint : fns.back_sale_group_inventory_item_import_create
(+) Postgres fin_sprint : fns.back_sale_group_inventory_item_import_delete

-- 2023-09-22 - KhoaBD - FAS-278 : [BE] API tìm kiếm vendor trên FIN - feature/20230915-MasterData
(+) Postgres fin_sprint : fns.vendor_master_read_for_update_vendor
(+) Postgres fin_sprint : fns.vendor_category_read_all
-- 2023-09-22 - KhoaBD - FAS-271 : [BE] API update chi tiết 1 vendor trên FIN - feature/20230915-MasterData
(+) Postgres fin_sprint : fns.vendor_master_update_detail
-- 2023-09-22 - KhoaBD - FAS-279: [BE] API xem chi tiết 1 Vendor trên FIN - feature/20230915-MasterData
 (+)  Postgres fin_sprint : fns.vendor_master_read_by_id
-- 2023-09-26 - KhoaBD - FAS-317 : [BE] API cập nhật vendo, import dữ liệu bằng file excel - feature/20230915-MasterData
(+)Postgres fin_sprint : fns.vendor_subsidiary_relationship_upsert


-- 2023-10-23 - TaiPM - Create FIN schedule service
Postgres fin_sprint
(+)schedule_get_more_than_1_minute_difference_schedules_for_ws
(+)schedule_get_valid_for_ws
(+)schedule_get_within_1_minute_difference_schedules_for_ws

-- 2023/10/27 - BaoNN - FIN-847 : [BE] API quản lý sale channel - 20230915-MasterData
(+) Postgres fin_sprint : fns.sale_channel_read_all
(+) Postgres fin_sprint : fns.sale_channel_read_by_id
(+) Postgres fin_sprint : fns.sale_channel_search
(+) Postgres fin_sprint : fns.sale_channel_create
(+) Postgres fin_sprint : fns.sale_channel_create_validate

(+) Postgres fin_sprint : scm.output_type_read_all
(+) Postgres fin_sprint : fin.sale_channel_map_read_by_sale_channel_id