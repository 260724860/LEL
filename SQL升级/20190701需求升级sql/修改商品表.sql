ALTER TABLE `le_goods`
	ADD COLUMN `Integral` INT(11) NOT NULL DEFAULT '0' COMMENT '����' AFTER `MinimumPurchase`,
	ADD COLUMN `IsCrossdomain` INT(11) NOT NULL DEFAULT '0' COMMENT '�Ƿ���Կ�������' AFTER `Integral`,
	ADD COLUMN `IsReturn` INT(11) NOT NULL DEFAULT '0' COMMENT '�Ƿ�����˻�' AFTER `IsCrossdomain`,
	ADD COLUMN `UrgentOrder` VARCHAR(50) NOT NULL DEFAULT '0' COMMENT '������' AFTER `IsReturn`,
	ADD COLUMN `SeckillBeginTime` DATETIME NULL COMMENT '��ɱ��ʼʱ��' AFTER `UrgentOrder`,
	ADD COLUMN `SeckillEndTime` DATETIME NULL COMMENT '��ɱ����ʱ��' AFTER `SeckillBeginTime`,
	ADD COLUMN `Initial` VARCHAR(20) NULL COMMENT '��Ʒ����ĸ' AFTER `SeckillEndTime`,
	ADD COLUMN `PlaceofOrigin` VARCHAR(20) NULL COMMENT '����' AFTER `Initial`,
	ADD COLUMN `ProductionDate` DATE NULL COMMENT '��������' AFTER `PlaceofOrigin`,
	ADD COLUMN `VirtualNumber` INT NOT NULL DEFAULT '0' COMMENT '������' AFTER `ProductionDate`,
	ADD COLUMN `SupplierCount` INT NOT NULL DEFAULT '1' COMMENT '��Ӧ����' AFTER `VirtualNumber`,
	ADD COLUMN `Remarks` VARCHAR(100) NOT NULL DEFAULT '1' COMMENT '��ע' AFTER `SupplierCount`;
	ADD COLUMN `GoodType` INT NOT NULL DEFAULT '1' COMMENT '��Ʒ���ͣ�1������2������3ɢ����' AFTER `Remarks`;

                ALTER TABLE `le_goods`
	CHANGE COLUMN `GoodType` `GoodsType` INT(11) NOT NULL DEFAULT '1' COMMENT '��Ʒ���ͣ�1������2������3ɢ����,
	ADD COLUMN `GoodsBarand` INT(11) NULL COMMENT '��ƷƷ��' ,
	ADD CONSTRAINT `FK_le_goods_le_goods_brand` FOREIGN KEY (`GoodsBarand`) REFERENCES `le_goods_brand` (`ID`);

ALTER TABLE `le_goods`
	ADD COLUMN `PriceFull` DECIMAL(9,2) NOT NULL DEFAULT '0' COMMENT '����������' ,
	ADD COLUMN `PriceReduction` DECIMAL(9,2) NOT NULL DEFAULT '0' COMMENT '����������' ,
	ADD COLUMN `TermOfValidity` VARCHAR(50) NOT NULL DEFAULT '0' COMMENT '��Ч��' ;


