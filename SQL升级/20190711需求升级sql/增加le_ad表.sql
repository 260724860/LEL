ALTER TABLE `le_ad`
	ADD COLUMN `PromotionBeginTime` DATETIME NULL DEFAULT NULL COMMENT '������ʼʱ��' AFTER `SupplierID`,
	ADD COLUMN `PromotionEndTime` DATETIME NULL DEFAULT NULL COMMENT '��������ʱ��' AFTER `PromotionBeginTime`,
	ADD COLUMN `GoodsID` INT NULL DEFAULT NULL COMMENT '��ƷID' AFTER `PromotionEndTime`;
