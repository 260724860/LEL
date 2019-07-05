ALTER TABLE `le_orders_head`
	ALTER `Money` DROP DEFAULT,
	ALTER `SupplyMoney` DROP DEFAULT,
	ALTER `GoodsCount` DROP DEFAULT;
ALTER TABLE `le_orders_head`
	ADD COLUMN `OrderSupplyAmount` DECIMAL(9,2) NOT NULL COMMENT '下单供应金额' AFTER `RcPhone`,
	ADD COLUMN `OrderAmout` DECIMAL(9,2) NOT NULL COMMENT '下单金额' AFTER `OrderSupplyAmount`,
	CHANGE COLUMN `Money` `RealAmount` DECIMAL(9,2) NOT NULL COMMENT '实际订单金额' AFTER `OrderAmout`,
	CHANGE COLUMN `SupplyMoney` `RealSupplyAmount` DECIMAL(9,2) NOT NULL COMMENT '实际供应商金额' AFTER `RealAmount`,
	CHANGE COLUMN `GoodsCount` `GoodsCount` INT(11) NOT NULL COMMENT '下单商品数量' AFTER `CarNumber`;
