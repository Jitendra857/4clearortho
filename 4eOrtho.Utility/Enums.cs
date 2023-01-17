namespace _4eOrtho.Utility
{
    public enum UserAccessType
    {
        SuperAdmin = 1,
        SiteAdmin = 2,
        SiteUser = 3,
        Driver = 4
    }

    public enum MessageType
    {
        Help,
        Error,
        ForgotError,
        Warning,
        Success,
        ForgotSuccess,
    }

    /// <summary>
    /// emr and aad integration for 4clear ortho
    /// </summary>
    public enum SourceType
    {
        EMR,
        AAAD,
        Ortho
    }
    /// <summary>
    ///P-Patient,
    ///D-Doctor,
    ///C-Clinical Assistant,
    ///F-Front Desk,
    ///A-Collection Agent,
    ///M-Management(admin of EMR)
    /// </summary>
    public enum UserType
    {
        /*
         P-Patient
         D-Doctor,Student(doctor)
         C-Clinical Assistant
         F-Front Desk
         A-Public All
         M-Management(admin of EMR)
         I-Mobile
         LC-LocalContactUser
         AU-AdminUser
         */
        P,
        D,
        S,
        SuperAdmin,
        A,
        AU,
        LC
    }


    public enum PatientType
    {
        /*
         P-Patient
         I-InActive
         N-Not Patient

         */
        P,
        I,
        N
    }

    public enum TreatmentPlanStatus
    {
        /*
         T-Treatment Plan
         C-Completed
         E-Existing
         EI-Existing Other
         I - Initial
         M - Misellinuous
         */
        T,
        C,
        E,
        EO,
        I,
        M
    }

    public enum InfectionPosition
    {
        /*
        M- Mesial or Left side
        D- Distal or Right side
        T-Top side
        B-Bottom side
        C-Center
        */
        M,
        D,
        B,
        T,
        C
    }

    public enum InfectionArea
    {
        /*
        B-Buccal or Facial
        I-Incisal or Lingual
        L-Lingual or Tongue
        O-Occlusal
        */
        B,
        I,
        L,
        O
    }

    public enum DesignPattern
    {
        /*
        H,
        V,
        D,
        C,
        R,
        T,
        RC
         */

        /* new enums  */
        /*
         S - Sealant
         RC - Root Canal
         RCS - Root Canal with Screw
         RCM - Root Canal with Mid Screw
         CR - Crowning
         C - Circle
         TE - Teeth Extracted (teeth will be disappeared)
         E - Extraction (2 vertical lines on the root)
         D - Denture
         B - Bridge
         SI - Surgical Placement Implant Body
         A - Amalgam (filling)
         */

        S,
        RC,
        RCS,
        RCM,
        CR,
        C,
        TE,
        E,
        D,
        B,
        SI,
        A
    }

    public enum OrthoSystem
    {
        TRAY = 1,
        BRACKET = 2
    }
    public enum OrthoCondition
    {
        CROWDING = 1,
        SPACING = 2,
        CROSSBITE = 3,
        ANTERIOR = 4,
        POSTERIOR = 5,
        OPENBITE = 6,
        DEEPBITE = 7,
        NARROWARCH = 8
    }

    public enum TrackingStatus
    {
        Submitted = 1,
        Acknowledged = 2,
        InProcess = 3,
        Shipped = 4,
        Received = 5
    }

    public enum StageStatus
    {
        Submitted = 1,        
        InProcess = 2,
        Completed = 3
    }
}