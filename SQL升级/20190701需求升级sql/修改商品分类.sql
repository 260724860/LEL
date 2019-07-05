ALTER TABLE `le_goodsgroups`
	ADD COLUMN `Img` VARCHAR(50) NULL COMMENT 'Í¼Æ¬' AFTER `CreateTime`,
	ADD COLUMN `Remarks` VARCHAR(50) NULL COMMENT '±¸×¢' AFTER `Img`;
