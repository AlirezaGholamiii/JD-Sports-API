using System;
using System.ComponentModel.DataAnnotations;

namespace JdRunner.Models
{
    //Table: USER_SESSIONS
    public class UserSessions
    {
        //CLIENT_ID - Primary Key
        [Required]
        [StringLength(50)]
        public String clientId { get; set; }
        //BUSINESS_UNIT_ID - Primary Key
        [Required]
        public int businessUnitId { get; set; }
        //USER_ID - Primary Key
        [Required]
        public Int32 userId { get; set; }
        //SESSION_ID - Primary Key
        [Required]
        [StringLength(256)]
        public String sessionId { get; set; }
        //ACTIVE_SESSION_IND - Mandatory (Not Null)
        [Required]
        [StringLength(1)]
        [RegularExpression("Y|N")]
        public String activeSessionInd { get; set; }
        //CREATION_DATE - Mandatory (Not Null)
        [Required]
        [DataType(DataType.Date)]
        public DateTime creationDate { get; set; }
        //LAST_ACTIVE_DATE - Mandatory (Not Null)
        [Required]
        [DataType(DataType.Date)]
        public DateTime lastActiveDate { get; set; }
        //EXPIRATION_DATE - Mandatory (Not Null)
        [Required]
        [DataType(DataType.Date)]
        public DateTime expirationDate { get; set; }
        //ACTIVE_IND - Mandatory (Not Null)
        [Required]
        [StringLength(1)]
        [RegularExpression("Y|N")]
        public String activeInd { get; set; }
        //ORIGIN - Mandatory (Not Null)
        [Required]
        [StringLength(30)]
        public String origin { get; set; }
        //CREATED_BY - Mandatory (Not Null)
        [Required]
        [StringLength(30)]
        public String createdBy { get; set; }
        //CREATED_DATE - Mandatory (Not Null)
        [Required]
        [DataType(DataType.Date)]
        public DateTime createdDate { get; set; }
        //MODIFIED_BY
        [StringLength(30)]
        public String modifiedBy { get; set; }
        //MODIFIED_DATE
        [DataType(DataType.Date)]
        public DateTime? modifiedDate { get; set; }
        //LOCK_COUNTER - Mandatory (Not Null)
        [Required]
        public Int16 lockCounter { get; set; }
        //USERS.FIRST_NAME
        public String usersUserName { get; set; }

    }
}
