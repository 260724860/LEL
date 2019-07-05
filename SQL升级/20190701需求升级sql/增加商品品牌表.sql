CREATE TABLE `le_Goods_Brand` (
	`ID` INT NOT NULL,
	`BrandName` VARCHAR(50) NOT NULL COMMENT '商品品牌名',
	`CreateTime` INT NOT NULL COMMENT '创建时间',
	PRIMARY KEY (`ID`)
)
COMMENT='商品品牌表'
COLLATE='utf8_general_ci'
ENGINE=InnoDB
;
