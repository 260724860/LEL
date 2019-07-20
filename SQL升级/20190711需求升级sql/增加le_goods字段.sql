ALTER TABLE `le_goods`
	CHANGE COLUMN `IsCrossdomain` `IsCrossdomain` INT(11) NOT NULL DEFAULT '0' COMMENT '�Ƿ���Կ�������' AFTER `IsDeliverHome`,
	CHANGE COLUMN `IsReturn` `IsReturn` INT(11) NOT NULL DEFAULT '0' COMMENT '�Ƿ�����˻�' AFTER `IsCrossdomain`,
	ADD COLUMN `IsParcel` INT NOT  NULL COMMENT '�Ƿ�ƴ����0/1��' AFTER `IsReturn`,
	ADD COLUMN `IsRandomDistribution` INT NOT  NULL COMMENT '�Ƿ���������0/1��' AFTER `IsParcel`,
	ADD COLUMN `Province` VARCHAR(50) NOT  NULL COMMENT 'ʡ' AFTER `TermOfValidity`,
	ADD COLUMN `City` VARCHAR(50) NULL COMMENT '��' AFTER `Province`,
	ADD COLUMN `Area` VARCHAR(50) NULL COMMENT '��' AFTER `City`,
	ADD COLUMN `PiecePrice` DECIMAL(9,2) NULL COMMENT '����' AFTER `Area`,
	ADD COLUMN `MinimumPrice` DECIMAL(9,2) NULL COMMENT '��С�����' AFTER `PiecePrice`,
	ADD COLUMN `BusinessValue` INT NULL COMMENT 'ҵ��ֵ' AFTER `MinimumPrice`,
	ADD COLUMN `NewPeriod` INT NULL COMMENT '��Ʒ�ڣ��죩' AFTER `BusinessValue`,
	ADD COLUMN `Unit` VARCHAR(50) NULL COMMENT '��λ' AFTER `NewPeriod`,
	ADD COLUMN `PriceScheme1` DECIMAL(9,2) NOT NULL DEFAULT '0'  COMMENT '�۸񷽰�1' AFTER `Unit`,
	ADD COLUMN `PriceScheme2` DECIMAL(9,2) NOT NULL DEFAULT '0'  COMMENT '�۸񷽰�2' AFTER `PriceScheme1`,
	ADD COLUMN `PriceScheme3` DECIMAL(9,2) NOT NULL DEFAULT '0'  COMMENT '�۸񷽰�3' AFTER `PriceScheme2`;

ALTER TABLE `le_goods`
	CHANGE COLUMN `IsParcel` `IsParcel` INT(11) NOT NULL DEFAULT '0' COMMENT '�Ƿ�ƴ����0/1��' AFTER `IsReturn`;


ALTER TABLE `le_goods`
	CHANGE COLUMN `PriceScheme1` `PriceScheme1` DECIMAL(9,2) NOT NULL DEFAULT '0' COMMENT '�۸񷽰�1' AFTER `Unit`,
	CHANGE COLUMN `PriceScheme2` `PriceScheme2` DECIMAL(9,2) NOT NULL DEFAULT '0' COMMENT '�۸񷽰�2' AFTER `PriceScheme1`,
	CHANGE COLUMN `PriceScheme3` `PriceScheme3` DECIMAL(9,2) NOT NULL DEFAULT '0' COMMENT '�۸񷽰�3' AFTER `PriceScheme2`;


