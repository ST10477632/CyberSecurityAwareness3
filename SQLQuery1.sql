create database cyberbotbot_db;

use cyberbot_db;

create table tasks (
id  int primary key,
title varchar(200) not null,
description text,
reminder varchar(200),
is_completed tinyint(1) default 0,
created_at datetime default current_timestamp
);