ALTER TABLE `le_goods`
	ADD COLUMN `QuotaBeginTime` DATETIME NULL COMMENT '限购开始时间' AFTER `PriceScheme3`,
	ADD COLUMN `QuotaEndTime` DATETIME NULL COMMENT '限购结束时间' AFTER `QuotaBeginTime`;
