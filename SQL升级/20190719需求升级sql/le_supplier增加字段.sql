ALTER TABLE `le_suppliers`
	ADD COLUMN `CustomerService` VARCHAR(20) NULL DEFAULT NULL COMMENT '客服姓名' AFTER `DelivererPhone`,
	ADD COLUMN `CustomerServicePhone` VARCHAR(20) NULL DEFAULT NULL COMMENT '客服电话' AFTER `CustomerService`,
	ADD COLUMN `Docker` VARCHAR(20) NULL DEFAULT NULL COMMENT '对接人' AFTER `CustomerServicePhone`,
	ADD COLUMN `DockerPhone` VARCHAR(20) NULL DEFAULT NULL COMMENT '对接人电话' AFTER `Docker`,
	ADD COLUMN `Zoning` VARCHAR(20) NULL DEFAULT NULL COMMENT '划分区域' AFTER `DockerPhone`,
	ADD COLUMN `CartModel` VARCHAR(20) NULL DEFAULT NULL COMMENT '车型' AFTER `Zoning`,
	ADD COLUMN `Classify` VARCHAR(20) NULL DEFAULT NULL COMMENT '分类' AFTER `CartModel`,
	ADD COLUMN `AnotherName` VARCHAR(20) NULL DEFAULT NULL COMMENT '别名' AFTER `ManagingBrands`;
