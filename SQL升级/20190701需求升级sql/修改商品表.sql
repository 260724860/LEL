ALTER TABLE `le_goods`
	ADD COLUMN `Integral` INT(11) NOT NULL DEFAULT '0' COMMENT '积分' AFTER `MinimumPurchase`,
	ADD COLUMN `IsCrossdomain` INT(11) NOT NULL DEFAULT '0' COMMENT '是否可以跨区进货' AFTER `Integral`,
	ADD COLUMN `IsReturn` INT(11) NOT NULL DEFAULT '0' COMMENT '是否可以退货' AFTER `IsCrossdomain`,
	ADD COLUMN `UrgentOrder` VARCHAR(50) NOT NULL DEFAULT '0' COMMENT '急单码' AFTER `IsReturn`,
	ADD COLUMN `SeckillBeginTime` DATETIME NULL COMMENT '秒杀开始时间' AFTER `UrgentOrder`,
	ADD COLUMN `SeckillEndTime` DATETIME NULL COMMENT '秒杀结束时间' AFTER `SeckillBeginTime`,
	ADD COLUMN `Initial` VARCHAR(20) NULL COMMENT '商品首字母' AFTER `SeckillEndTime`,
	ADD COLUMN `PlaceofOrigin` VARCHAR(20) NULL COMMENT '产地' AFTER `Initial`,
	ADD COLUMN `ProductionDate` DATE NULL COMMENT '生成日期' AFTER `PlaceofOrigin`,
	ADD COLUMN `VirtualNumber` INT NOT NULL DEFAULT '0' COMMENT '虚拟数' AFTER `ProductionDate`,
	ADD COLUMN `SupplierCount` INT NOT NULL DEFAULT '1' COMMENT '供应商数' AFTER `VirtualNumber`,
	ADD COLUMN `Remarks` VARCHAR(100) NOT NULL DEFAULT '1' COMMENT '备注' AFTER `SupplierCount`;
	ADD COLUMN `GoodType` INT NOT NULL DEFAULT '1' COMMENT '商品类型（1有条码2无条码3散货）' AFTER `Remarks`;

                ALTER TABLE `le_goods`
	CHANGE COLUMN `GoodType` `GoodsType` INT(11) NOT NULL DEFAULT '1' COMMENT '商品类型（1有条码2无条码3散货）,
	ADD COLUMN `GoodsBarand` INT(11) NULL COMMENT '商品品牌' ,
	ADD CONSTRAINT `FK_le_goods_le_goods_brand` FOREIGN KEY (`GoodsBarand`) REFERENCES `le_goods_brand` (`ID`);

ALTER TABLE `le_goods`
	ADD COLUMN `PriceFull` DECIMAL(9,2) NOT NULL DEFAULT '0' COMMENT '满减（满）' ,
	ADD COLUMN `PriceReduction` DECIMAL(9,2) NOT NULL DEFAULT '0' COMMENT '满减（减）' ,
	ADD COLUMN `TermOfValidity` VARCHAR(50) NOT NULL DEFAULT '0' COMMENT '有效期' ;


