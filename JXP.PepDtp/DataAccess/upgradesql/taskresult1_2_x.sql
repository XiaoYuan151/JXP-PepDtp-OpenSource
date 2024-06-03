PRAGMA foreign_keys = 0;

CREATE TABLE taskresult_temp AS SELECT *
                                          FROM taskresult;

DROP TABLE taskresult;

CREATE TABLE taskresult (
    resultid   VARCHAR (128) PRIMARY KEY
                             NOT NULL,
    userid     VARCHAR (32),
    title      VARCHAR (64),
    fileformat VARCHAR (10),
    result     VARCHAR (10) 
);

INSERT INTO taskresult (
                           resultid,
                           userid,
                           title,
                           fileformat,
                           result
                       )
                       SELECT resultid,
                              userid,
                              title,
                              fileformat,
                              result
                         FROM taskresult_temp;

DROP TABLE taskresult_temp;
PRAGMA foreign_keys = 1;
