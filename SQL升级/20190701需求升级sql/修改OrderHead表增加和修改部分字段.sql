ALTER TABLE `le_orders_head`
	ALTER `Money` DROP DEFAULT,
	ALTER `SupplyMoney` DROP DEFAULT,
	ALTER `GoodsCount` DROP DEFAULT;
ALTER TABLE `le_orders_head`
	ADD COLUMN `OrderSupplyAmount` DECIMAL(9,2) NOT NULL COMMENT '�µ���Ӧ���' AFTER `RcPhone`,
	ADD COLUMN `OrderAmout` DECIMAL(9,2) NOT NULL COMMENT '�µ����' AFTER `OrderSupplyAmount`,
	CHANGE COLUMN `Money` `RealAmount` DECIMAL(9,2) NOT NULL COMMENT 'ʵ�ʶ������' AFTER `OrderAmout`,
	CHANGE COLUMN `SupplyMoney` `RealSupplyAmount` DECIMAL(9,2) NOT NULL COMMENT 'ʵ�ʹ�Ӧ�̽��' AFTER `RealAmount`,
	CHANGE COLUMN `GoodsCount` `GoodsCount` INT(11) NOT NULL COMMENT '�µ���Ʒ����' AFTER `CarNumber`;
