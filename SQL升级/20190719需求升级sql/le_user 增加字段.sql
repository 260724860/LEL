ALTER TABLE `le_users`
	ADD COLUMN `Zoning` VARCHAR(20) NULL DEFAULT NULL COMMENT '划分区域' AFTER `Remarks`,
	ADD COLUMN `CartModel` VARCHAR(20) NULL DEFAULT NULL COMMENT '车型' AFTER `Zoning`,
	ADD COLUMN `ContractNumber` VARCHAR(20) NULL DEFAULT NULL COMMENT '合同编号' AFTER `CartModel`,
	ADD COLUMN `Classify` VARCHAR(20) NULL DEFAULT NULL COMMENT '分类' AFTER `ContractNumber`;
                ADD COLUMN `AnotherName` VARCHAR(20) NULL DEFAULT NULL COMMENT '别名' AFTER `ManagingBrands`;
