ALTER TABLE `le_sys_address`
	ADD COLUMN `Img` VARCHAR(100) NULL DEFAULT NULL COMMENT '图片' AFTER `Latitude`,
	ADD COLUMN `EmergencyContactPhone` VARCHAR(50) NULL DEFAULT NULL COMMENT '紧急联系电话' AFTER `Img`,
	ADD COLUMN `Remarks` VARCHAR(50) NULL DEFAULT NULL COMMENT '备注' AFTER `EmergencyContactPhone`;
