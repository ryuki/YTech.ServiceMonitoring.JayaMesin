USE [DB_WOT_JAYA_MESIN]
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_LIST_WO_READ]    Script Date: 03/16/2014 09:43:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GET_LIST_WO_READ]
	-- Add the parameters for the stored procedure here
	@User_Name nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT wo.*, WO.wo_id,wo.wo_no , a.log_id, a.LOG_USER,a.log_status, a.log_date, 
b.log_id, b.LOG_USER, b.log_status, b.log_date ,
c.customer_name,c.customer_address, c.customer_phone,
case 
when a.LOG_USER = @User_Name then 'true' 
when a.log_date < a.log_date then 'true'
else 'false' end HaveBeenRead
FROM T_WO WO
LEFT JOIN (select l.*, dense_rank() over (partition by wo_id order by row_version desc) ranking
from t_wo_log l
where l.log_status <> 'Read'
) a
ON WO.WO_ID = a.WO_ID
and a.ranking = 1

LEFT JOIN (select l.*, dense_rank() over (partition by wo_id order by row_version desc) ranking
from t_wo_log l
where l.log_status = 'Read'
and l.log_user = @User_Name
) b
ON WO.WO_ID = b.WO_ID
and b.ranking = 1

left join m_customer c
on wo.customer_id = c.customer_id

where wo.data_status <> 'Deleted'
--wo.wo_id = '05e21ab2-5f90-4851-be3e-2e53369bf36d'
order by HaveBeenRead, wo.wo_priority desc, wo.wo_no desc

END
GO
