ALTER TABLE `le_users`
	ADD COLUMN `ReceiveName` VARCHAR(20) NULL DEFAULT NULL COMMENT '收货人' AFTER `AnotherName`,
	ADD COLUMN `ReceivePhone` VARCHAR(20) NULL DEFAULT NULL COMMENT '收货人电话' AFTER `ReceiveName`;



ALTER TABLE `le_users`
	ADD COLUMN `CustomerService` VARCHAR(20) NULL DEFAULT NULL COMMENT '客服' AFTER `ReceivePhone`,
	ADD COLUMN `CustomerServicePhone` VARCHAR(20) NULL DEFAULT NULL COMMENT '客服电话' AFTER `CustomerService`;
