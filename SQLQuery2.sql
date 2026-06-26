CREATE DATABASE cyberbot_db;

USE [cyber_db];

CREATE TABLE tasks (
    id INT IDENTITY(1,1) PRIMARY KEY,
    title VARCHAR(200) NOT NULL,
    description VARCHAR(MAX),
    reminder VARCHAR(200),
    is_completed BIT DEFAULT 0,
    task_dueDate varchar(20)
);

select * from tasks;