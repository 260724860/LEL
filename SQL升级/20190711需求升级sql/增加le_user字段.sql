ALTER TABLE `le_users`
	ADD COLUMN `ReceiveName` VARCHAR(20) NULL DEFAULT NULL COMMENT '�ջ���' AFTER `AnotherName`,
	ADD COLUMN `ReceivePhone` VARCHAR(20) NULL DEFAULT NULL COMMENT '�ջ��˵绰' AFTER `ReceiveName`;



ALTER TABLE `le_users`
	ADD COLUMN `CustomerService` VARCHAR(20) NULL DEFAULT NULL COMMENT '�ͷ�' AFTER `ReceivePhone`,
	ADD COLUMN `CustomerServicePhone` VARCHAR(20) NULL DEFAULT NULL COMMENT '�ͷ��绰' AFTER `CustomerService`;
