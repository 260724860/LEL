ALTER TABLE `le_orders_lines`
	ALTER `Money` DROP DEFAULT;
ALTER TABLE `le_orders_lines`
	CHANGE COLUMN `Money` `GoodsPrice` DECIMAL(9,2) NOT NULL COMMENT '��Ʒ����' AFTER `GoodsID`;
ALTER TABLE `le_orders_lines`
	CHANGE COLUMN `Profit` `Profit` DECIMAL(9,2) NOT NULL DEFAULT '0.00' COMMENT '����=Money-supplyPrice' AFTER `DeliverCount`,
	

