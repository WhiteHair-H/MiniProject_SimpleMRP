-- 1. PrcResult에서 성공 갯수와 실패갯수를 다른 컬럼으로 분리 결과
-- 가상테이블 생성

SELECT p.Schidx , p.PrcDate , 
	  CASE p.PrcResult WHEN 1 THEN 1 else 0 END AS PrcOK,
	  CASE p.PrcResult WHEN 0 THEN 1 else 0 END AS PrcFail
  FROM Process AS p

-- 2. 합계집계 결과 가상테이블

SELECT smr.Schidx , smr.PrcDate , 
	   sum(smr.PrcOK) as PrcOKAmount , sum(smr.PrcFail) as PrcFAILAmount
  FROM( 
	  SELECT p.Schidx , p.PrcDate , 
		  CASE p.PrcResult WHEN 1 THEN 1 else 0 END AS PrcOK,
		  CASE p.PrcResult WHEN 0 THEN 1 else 0 END AS PrcFail
	  FROM Process AS p
		) AS smr
group by smr.Schidx, smr.PrcDate

-- 3.0 조인문
SELECT * FROM Schedules AS sch Inner JOIN Process AS prc
	ON sch.SchIdx = prc.Schidx

-- 3. 2번결과(가상테이블)와 Schdules 테이블 조인해서 원하는 결과도출

SELECT  sch.SchIdx ,sch.PlantCode ,
		sch.SchAmount , prc.PrcDate ,
		prc.PrcOKAmount , prc.PrcFAILAmount
  FROM Schedules AS sch
  INNER JOIN (
			  SELECT smr.Schidx , smr.PrcDate , 
				   SUM(smr.PrcOK) AS PrcOKAmount , SUM(smr.PrcFail) AS PrcFAILAmount
			    FROM( 
				  SELECT p.Schidx , p.PrcDate , 
					  CASE p.PrcResult WHEN 1 THEN 1 ELSE 0 END AS PrcOK,
					  CASE p.PrcResult WHEN 0 THEN 1 ELSE 0 END AS PrcFail
				  FROM Process AS p
					) AS smr
			GROUP BY smr.Schidx, smr.PrcDate
	) AS prc
	ON sch.SchIdx = prc.Schidx