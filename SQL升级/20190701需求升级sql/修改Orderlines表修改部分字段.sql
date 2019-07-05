ALTER TABLE `le_orders_lines`
	ALTER `Money` DROP DEFAULT;
ALTER TABLE `le_orders_lines`
	CHANGE COLUMN `Money` `GoodsPrice` DECIMAL(9,2) NOT NULL COMMENT '商品单价' AFTER `GoodsID`;
