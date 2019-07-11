CREATE TABLE `le_sysversion` (
	`ID` INT(11) NOT NULL AUTO_INCREMENT,
	`ViversionNumber` VARCHAR(50) NOT NULL COMMENT '版本号',
	`Description` VARCHAR(1024) NOT NULL COMMENT '说明',
	`LeftoverBug` VARCHAR(1024) NOT NULL COMMENT '遗留问题bug',
	`CreateTime` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
	`UpdateTime` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最后上传时间',
	`Remarks` VARCHAR(50) NOT NULL COMMENT '备注',
	PRIMARY KEY (`ID`)
)
COMMENT='当前系统运行版本说明'
ENGINE=InnoDB
;
