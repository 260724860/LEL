ALTER TABLE `le_goods`
	CHANGE COLUMN `IsCrossdomain` `IsCrossdomain` INT(11) NOT NULL DEFAULT '0' COMMENT '是否可以跨区进货' AFTER `IsDeliverHome`,
	CHANGE COLUMN `IsReturn` `IsReturn` INT(11) NOT NULL DEFAULT '0' COMMENT '是否可以退货' AFTER `IsCrossdomain`,
	ADD COLUMN `IsParcel` INT NOT  NULL COMMENT '是否拼货（0/1）' AFTER `IsReturn`,
	ADD COLUMN `IsRandomDistribution` INT NOT  NULL COMMENT '是否随机配货（0/1）' AFTER `IsParcel`,
	ADD COLUMN `Province` VARCHAR(50) NOT  NULL COMMENT '省' AFTER `TermOfValidity`,
	ADD COLUMN `City` VARCHAR(50) NULL COMMENT '市' AFTER `Province`,
	ADD COLUMN `Area` VARCHAR(50) NULL COMMENT '区' AFTER `City`,
	ADD COLUMN `PiecePrice` DECIMAL(9,2) NULL COMMENT '件价' AFTER `Area`,
	ADD COLUMN `MinimumPrice` DECIMAL(9,2) NULL COMMENT '最小起配价' AFTER `PiecePrice`,
	ADD COLUMN `BusinessValue` INT NULL COMMENT '业务值' AFTER `MinimumPrice`,
	ADD COLUMN `NewPeriod` INT NULL COMMENT '新品期（天）' AFTER `BusinessValue`,
	ADD COLUMN `Unit` VARCHAR(50) NULL COMMENT '单位' AFTER `NewPeriod`,
	ADD COLUMN `PriceScheme1` DECIMAL(9,2) NOT NULL DEFAULT '0'  COMMENT '价格方案1' AFTER `Unit`,
	ADD COLUMN `PriceScheme2` DECIMAL(9,2) NOT NULL DEFAULT '0'  COMMENT '价格方案2' AFTER `PriceScheme1`,
	ADD COLUMN `PriceScheme3` DECIMAL(9,2) NOT NULL DEFAULT '0'  COMMENT '价格方案3' AFTER `PriceScheme2`;

ALTER TABLE `le_goods`
	CHANGE COLUMN `IsParcel` `IsParcel` INT(11) NOT NULL DEFAULT '0' COMMENT '是否拼货（0/1）' AFTER `IsReturn`;


ALTER TABLE `le_goods`
	CHANGE COLUMN `PriceScheme1` `PriceScheme1` DECIMAL(9,2) NOT NULL DEFAULT '0' COMMENT '价格方案1' AFTER `Unit`,
	CHANGE COLUMN `PriceScheme2` `PriceScheme2` DECIMAL(9,2) NOT NULL DEFAULT '0' COMMENT '价格方案2' AFTER `PriceScheme1`,
	CHANGE COLUMN `PriceScheme3` `PriceScheme3` DECIMAL(9,2) NOT NULL DEFAULT '0' COMMENT '价格方案3' AFTER `PriceScheme2`;


