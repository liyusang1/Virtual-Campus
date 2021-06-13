INSERT INTO Community (userId, contentName, content)
VALUES (?,?,?);

-- 게시글 전체 조회
SELECT contentName,userNickname,IFNULL(replyCount,0) AS replyCount,
      CASE
           WHEN TIMESTAMPDIFF(HOUR, Community.createdAt, now()) > 23
               THEN IF(TIMESTAMPDIFF(DAY, Community.createdAt, now()) > 7, date_format(Community.createdAt, '%Y-%m-%d'),
                       concat(TIMESTAMPDIFF(DAY, Community.createdAt, now()), " 일 전"))
           WHEN TIMESTAMPDIFF(HOUR, Community.createdAt, now()) < 1
               THEN concat(TIMESTAMPDIFF(MINUTE, Community.createdAt, now()), " 분 전")
           ELSE concat(TIMESTAMPDIFF(HOUR, Community.createdAt, now()), " 시간 전")
      END AS createdAt
FROM Community
INNER JOIN User ON Community.userId = User.userId

LEFT OUTER JOIN (SELECT contentId, count(*) AS replyCount FROM CommunityReply WHERE status = 1
                            GROUP BY contentId) reply ON reply.contentId = Community.contentId
WHERE Community.status = 1
ORDER BY Community.createdAt DESC;
;


-- 게시글 상세 조회
SELECT contentName,content,userNickname,IFNULL(replyCount,0) AS replyCount,
      CASE
           WHEN TIMESTAMPDIFF(HOUR, Community.createdAt, now()) > 23
               THEN IF(TIMESTAMPDIFF(DAY, Community.createdAt, now()) > 7, date_format(Community.createdAt, '%Y-%m-%d'),
                       concat(TIMESTAMPDIFF(DAY, Community.createdAt, now()), " 일 전"))
           WHEN TIMESTAMPDIFF(HOUR, Community.createdAt, now()) < 1
               THEN concat(TIMESTAMPDIFF(MINUTE, Community.createdAt, now()), " 분 전")
           ELSE concat(TIMESTAMPDIFF(HOUR, Community.createdAt, now()), " 시간 전")
      END AS createdAt
FROM Community
INNER JOIN User ON Community.userId = User.userId

LEFT OUTER JOIN (SELECT contentId, count(*) AS replyCount FROM CommunityReply WHERE status = 1
                            GROUP BY contentId) reply ON reply.contentId = Community.contentId
WHERE Community.contentId = ? and Community.status = 1;

-- 게시글의 댓글 전달
SELECT replyId,userNickname,content,
      CASE
           WHEN TIMESTAMPDIFF(HOUR, CommunityReply.createdAt, now()) > 23
               THEN IF(TIMESTAMPDIFF(DAY, CommunityReply.createdAt, now()) > 7, date_format(CommunityReply.createdAt, '%Y-%m-%d'),
                       concat(TIMESTAMPDIFF(DAY, CommunityReply.createdAt, now()), " 일 전"))
           WHEN TIMESTAMPDIFF(HOUR, CommunityReply.createdAt, now()) < 1
               THEN concat(TIMESTAMPDIFF(MINUTE, CommunityReply.createdAt, now()), " 분 전")
           ELSE concat(TIMESTAMPDIFF(HOUR, CommunityReply.createdAt, now()), " 시간 전")
      END AS createdAt
from CommunityReply
INNER JOIN User ON CommunityReply.userId = User.userId
WHERE contentId = ?
ORDER BY CommunityReply.createdAt DESC;

SELECT count(1) FROM User WHERE userNickname = ?;


SELECT
       contentName,
       (SELECT content FROM CommunityReply WHERE Community.contentId = CommunityReply.contentId
         AND replyId = (SELECT MAX(replyId) FROM CommunityReply where CommunityReply.contentId = Community.contentId)
       )replyContent,
       createdAt
FROM Community;


DELETE FROM UserQuest WHERE userId=? AND questId=?;

SELECT userMoney,userNickname FROM User WHERE userId = ?;

UPDATE User SET userMoney = userMoney + ? WHERE userId = ?;

INSERT INTO CommunityReply(userId,contentId,content) VALUES (?, ?, ?);

SELECT userId FROM UserRoom WHERE userId = ?;

INSERT INTO UserRoom (userId)
  VALUES (?);

UPDATE UserRoom SET funiture1 = ? WHERE userId = ?;

UPDATE UserRoom SET funiture2 = ? WHERE userId = ?;

SELECT funiture1,funiture2,funiture3,funiture4,funiture5 FROM UserRoom WHERE userId = ?;

SELECT itemCode,itemCount FROM Inventory WHERE userId = ?;

SELECT COUNT(1) AS CNT FROM Inventory WHERE itemCode =? AND userId =? ;

UPDATE Inventory SET itemCount=itemCount+1 WHERE itemCode=? AND userId = ?;

INSERT INTO Inventory (itemCode, userId) VALUES (?,?);

UPDATE Inventory SET itemCount=itemCount-1 WHERE itemCode=? AND userId = ?;

DELETE FROM Inventory WHERE itemCode=? AND userId=?;

SELECT itemCount AS CNT FROM Inventory WHERE itemCode =? AND userId =?;

SELECT itemCode,itemCount FROM Inventory WHERE userId = ?;

SELECT COUNT(1) AS CNT FROM Inventory WHERE itemCode =? AND userId =?;

UPDATE Inventory SET itemCount=itemCount+1 WHERE itemCode=? AND userId = ?;

INSERT INTO Inventory (itemCode, userId) VALUES (?,?);

SELECT * FROM UserCustomizing WHERE userId = ?;

SELECT Count(1) as CNT FROM UserCustomizing WHERE userId = ?

INSERT INTO UserCustomizing (hairR, hairG, hairB, eyeR, eyeG, eyeB, topR, topG, topB, bottomR, bottomG, bottomB, shoeR, shoeG, shoeB,userId)
  VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)

 UPDATE UserCustomizing SET hairR = ?, hairG = ?, hairB=?, eyeR=?, eyeG=?, eyeB=?, topR=?, topG=?, topB=?, bottomR=?, bottomG=?,
                           bottomB=?, shoeR=?, shoeG=?, shoeB=?
  WHERE userId = ?;

INSERT INTO UserCustomizing (userId) VALUES (?);

SELECT questStatus FROM UserQuest WHERE userId = ? AND questId = ?;

INSERT INTO UserQuest (userId, questId) VALUES (?,?);

SELECT itemCount FROM Inventory WHERE itemCode = ? AND userId = ?;

UPDATE UserQuest SET questStatus = 1 WHERE userId= ?  AND questId = ?

  SELECT
  CASE
       WHEN TIMESTAMPDIFF(HOUR, UserQuest.createdAt, now()) < 23
           THEN 1
  END AS CNT
  FROM UserQuest WHERE userId = ? AND questId = ?;

DELETE FROM UserQuest WHERE userId=? AND questId=?;




