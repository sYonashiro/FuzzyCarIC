using UnityEngine;
using System.Collections;
using AForge;
using AForge.Fuzzy;

public class Fuzzy
{
    #region Variáveis privadas
    private FuzzySet fsNearF, fsMediumF, fsFarF, fsNearS, fsMediumS, fsFarS, fsslow, fsnormal, fsfast, fsVeryNearCF, fsVeryfast,
    fsVeryNegative,fsLittleNegative,fsVeryLittleNegative, fsNegative, fsZero, fsVeryLittlePositive, fsLittlePositive, fsPositive, fsVeryPositive, fsCurva, fsCurvaEsquerda, fsReta,fsVeryNearF
	, fsNearCF, fsMediumCF, fsFarCF,fsCarroAdFrontal,fsCarroAdLeft, fsCarroAdRight, fsCarroAdRightFront, fsCarroAdRightRear, fsCarroAdLeftFront, fsCarroAdLeftRear, fsCarroAdFrontalRight,
	fsCarroAdFrontalLeft, fsCarroAdCurvedFrontalRight, fsCarroAdCurvedFrontalLeft,fsStop;

	private LinguisticVariable lvFrontal, lvLeft, lvRight, lvAngle, lvRoadSegment,lvRightFront, lvRightRear, lvLeftFront, lvLeftRear, lvFrontalRight, lvFrontalLeft, lvCurvedFrontalRight
	, lvCurvedFrontalLeft, lvAcceleration,lvCarroAd;

    private Database DB;

    private InferenceSystem IS;
    #endregion

    public Fuzzy()
    {
        InitFuzzySets();
        InitLinguisticVariables();

        DB = new Database();
        DB.AddVariable(lvFrontal);
        DB.AddVariable(lvLeft);
        DB.AddVariable(lvRight);
        DB.AddVariable(lvAngle);
		DB.AddVariable(lvRoadSegment);
		DB.AddVariable (lvRightFront);
		DB.AddVariable (lvRightRear);
		DB.AddVariable (lvLeftFront);
		DB.AddVariable (lvLeftRear);
		DB.AddVariable (lvFrontalRight);
		DB.AddVariable (lvFrontalLeft);
		DB.AddVariable (lvCurvedFrontalRight);
		DB.AddVariable (lvCurvedFrontalLeft);
		DB.AddVariable (lvAcceleration);
		DB.AddVariable (lvCarroAd);
        IS = new InferenceSystem(DB, new CentroidDefuzzifier(1000));

        SetRules();
    }

    #region Métodos Privados
    /// <summary>
    /// Inicializa conjuntos fuzzy
    /// </summary>
    private void InitFuzzySets()
    {
		//Carro Adversário

		fsCarroAdFrontal = new FuzzySet("Frontal", new SingletonFunction(1)); 
		fsCarroAdRight = new FuzzySet("Right", new SingletonFunction(2)); 
		fsCarroAdLeft = new FuzzySet("Left", new SingletonFunction(3)); 
		fsCarroAdRightFront = new FuzzySet("RightFront", new SingletonFunction(4)); 
		fsCarroAdRightRear = new FuzzySet("RightRear", new SingletonFunction(5)); 
		fsCarroAdLeftFront = new FuzzySet("LeftFront", new SingletonFunction(6)); 
		fsCarroAdLeftRear = new FuzzySet("LeftRear", new SingletonFunction(7)); 
		fsCarroAdFrontalRight = new FuzzySet("FrontalRight", new SingletonFunction(8)); 
		fsCarroAdFrontalLeft = new FuzzySet("FrontalLeft", new SingletonFunction(9)); 
		fsCarroAdCurvedFrontalRight = new FuzzySet("CurvedFrontalRight", new SingletonFunction(10)); 
		fsCarroAdCurvedFrontalLeft = new FuzzySet("CurvedFrontalLeft", new SingletonFunction(11)); 


        // Frontal
		fsVeryNearF = new FuzzySet("VeryNearF", new TrapezoidalFunction(5,10, TrapezoidalFunction.EdgeType.Right));
        fsNearF = new FuzzySet("NearF", new TrapezoidalFunction(5,10,15, 30));
        fsMediumF = new FuzzySet("MediumF", new TrapezoidalFunction(15, 30, 45, 60));
        fsFarF = new FuzzySet("FarF", new TrapezoidalFunction(45, 60, TrapezoidalFunction.EdgeType.Left));

	    // Curved Frontal
		fsVeryNearCF = new FuzzySet("VeryNearCF", new TrapezoidalFunction(4,6, TrapezoidalFunction.EdgeType.Right));
		fsNearCF = new FuzzySet("NearCF", new TrapezoidalFunction(4,6,7,9));
		fsMediumCF = new FuzzySet("MediumCF", new TrapezoidalFunction(7, 9, 11, 13));
		fsFarCF = new FuzzySet("FarCF", new TrapezoidalFunction(11, 13, TrapezoidalFunction.EdgeType.Left));



        // Sides
        fsNearS = new FuzzySet("NearS", new TrapezoidalFunction(2, 4, TrapezoidalFunction.EdgeType.Right));
        fsMediumS = new FuzzySet("MediumS", new TrapezoidalFunction(2, 4, 8, 12));
        fsFarS = new FuzzySet("FarS", new TrapezoidalFunction(8, 12, TrapezoidalFunction.EdgeType.Left));

		// Road Segment
		fsReta = new FuzzySet("Reta", new SingletonFunction(0));
		fsCurva = new FuzzySet("Curva", new SingletonFunction(1));
		//fsCurvaEsquerda = new FuzzySet("CurvaEsquerda", new SingletonFunction(2));

        // Angle
        fsVeryNegative = new FuzzySet("VeryNegative", new TrapezoidalFunction(-70, -60, TrapezoidalFunction.EdgeType.Right));
		fsNegative = new FuzzySet("Negative", new TrapezoidalFunction(-70, -60, -40, -30));
        fsLittleNegative = new FuzzySet("LittleNegative", new TrapezoidalFunction(-40, -30, -25, -15));
		fsVeryLittleNegative = new FuzzySet("VeryLittleNegative", new TrapezoidalFunction(-25, -15, -10, 0));
        fsZero = new FuzzySet("Zero", new TrapezoidalFunction(-10, 0, 10));
		fsVeryLittlePositive = new FuzzySet("VeryLittlePositive", new TrapezoidalFunction(0, 10, 15, 25));
		fsLittlePositive = new FuzzySet("LittlePositive", new TrapezoidalFunction(15, 25, 30, 40));
		fsPositive = new FuzzySet("Positive", new TrapezoidalFunction(30, 40, 60, 70));
        fsVeryPositive = new FuzzySet("VeryPositive", new TrapezoidalFunction(60, 70, TrapezoidalFunction.EdgeType.Left));


		//Acceleration
		fsStop = new FuzzySet("Stop", new TrapezoidalFunction(1,2, TrapezoidalFunction.EdgeType.Right));
		fsslow = new FuzzySet("Slow", new TrapezoidalFunction(1,2,8,10));
		fsnormal = new FuzzySet("Normal", new TrapezoidalFunction(8, 10, 15, 20));
		fsfast = new FuzzySet("Fast", new TrapezoidalFunction(15, 20, 30,35));
		fsVeryfast = new FuzzySet("VeryFast", new TrapezoidalFunction( 30, 35,TrapezoidalFunction.EdgeType.Left));
    }

    /// <summary>
    /// Inicializa variáveis linguísticas
    /// </summary>
    private void InitLinguisticVariables()
    {
		//Carro Adversario

		lvCarroAd = new LinguisticVariable ("CarroAdversario", 0, 11);
		lvCarroAd.AddLabel(fsCarroAdFrontal);
		lvCarroAd.AddLabel(fsCarroAdRight);
		lvCarroAd.AddLabel(fsCarroAdLeft);
		lvCarroAd.AddLabel(fsCarroAdRightFront);
		lvCarroAd.AddLabel(fsCarroAdRightRear);
		lvCarroAd.AddLabel(fsCarroAdLeftFront);
		lvCarroAd.AddLabel(fsCarroAdLeftRear);
		lvCarroAd.AddLabel(fsCarroAdFrontalRight);
		lvCarroAd.AddLabel(fsCarroAdFrontalLeft);
		lvCarroAd.AddLabel(fsCarroAdCurvedFrontalRight);
		lvCarroAd.AddLabel(fsCarroAdCurvedFrontalLeft);


        // Frontal
        lvFrontal = new LinguisticVariable("Frontal", 0, 1000);
		lvFrontal.AddLabel(fsVeryNearF);
        lvFrontal.AddLabel(fsNearF);
        lvFrontal.AddLabel(fsMediumF);
        lvFrontal.AddLabel(fsFarF);

        // Left
        lvLeft = new LinguisticVariable("Left", 0, 500);
        lvLeft.AddLabel(fsNearS);
        lvLeft.AddLabel(fsMediumS);
        lvLeft.AddLabel(fsFarS);

        // Right
        lvRight = new LinguisticVariable("Right", 0, 500);
        lvRight.AddLabel(fsNearS);
        lvRight.AddLabel(fsMediumS);
        lvRight.AddLabel(fsFarS);

		// Right Front

		lvRightFront = new LinguisticVariable("RightFront", 0, 500);
		lvRightFront.AddLabel(fsNearS);
		lvRightFront.AddLabel(fsMediumS);
		lvRightFront.AddLabel(fsFarS);



		//Right Rear

		lvRightRear = new LinguisticVariable ("RightRear", 0, 500);
		lvRightRear.AddLabel (fsNearS);
		lvRightRear.AddLabel (fsMediumS);
		lvRightRear.AddLabel (fsFarS);

		//Left Front

		lvLeftFront = new LinguisticVariable ("LeftFront", 0, 500);
		lvLeftFront.AddLabel (fsNearS);
		lvLeftFront.AddLabel (fsMediumS);
		lvLeftFront.AddLabel (fsFarS);



		//Left Rear


		lvLeftRear = new LinguisticVariable ("LeftRear", 0, 500);
		lvLeftRear.AddLabel (fsNearS);
		lvLeftRear.AddLabel (fsMediumS);
		lvLeftRear.AddLabel (fsFarS);

		//Frontal Right
		lvFrontalRight = new LinguisticVariable ("FrontalRight", 0, 1000);
		lvFrontalRight.AddLabel(fsVeryNearF);
		lvFrontalRight.AddLabel(fsNearF);
		lvFrontalRight.AddLabel(fsMediumF);
		lvFrontalRight.AddLabel(fsFarF);
		//Frontal Left

		lvFrontalLeft = new LinguisticVariable ("FrontalLeft", 0, 1000);
		lvFrontalLeft.AddLabel(fsVeryNearF);
		lvFrontalLeft.AddLabel(fsNearF);
		lvFrontalLeft.AddLabel(fsMediumF);
		lvFrontalLeft.AddLabel(fsFarF);


		//Curved Frontal Right
		lvCurvedFrontalRight = new LinguisticVariable ("CurvedFrontalRight", 0, 1000);
		lvCurvedFrontalRight.AddLabel(fsVeryNearCF);
		lvCurvedFrontalRight.AddLabel(fsNearCF);
		lvCurvedFrontalRight.AddLabel(fsMediumCF);
		lvCurvedFrontalRight.AddLabel(fsFarCF);
		//Curved Frontal Left

		lvCurvedFrontalLeft = new LinguisticVariable ("CurvedFrontalLeft", 0, 1000);
		lvCurvedFrontalLeft.AddLabel(fsVeryNearCF);
		lvCurvedFrontalLeft.AddLabel(fsNearCF);
		lvCurvedFrontalLeft.AddLabel(fsMediumCF);
		lvCurvedFrontalLeft.AddLabel(fsFarCF);



		// Segment

		lvRoadSegment = new LinguisticVariable ("RoadSegment", 0, 1);
		lvRoadSegment.AddLabel (fsReta);
		lvRoadSegment.AddLabel (fsCurva);
		//lvRoadSegment.AddLabel (fsCurvaEsquerda);

        // Angle
        lvAngle = new LinguisticVariable("Angle", -90, 90);
        lvAngle.AddLabel(fsVeryNegative);
		lvAngle.AddLabel(fsNegative);
		lvAngle.AddLabel(fsLittleNegative);
		lvAngle.AddLabel (fsVeryLittleNegative);
		lvAngle.AddLabel(fsZero);
		lvAngle.AddLabel (fsVeryLittlePositive);
		lvAngle.AddLabel(fsLittlePositive);
        lvAngle.AddLabel(fsPositive);
        lvAngle.AddLabel(fsVeryPositive);

		// Acceleration

		lvAcceleration = new LinguisticVariable("Acceleration", 0, 40);
		lvAcceleration.AddLabel (fsStop);
		lvAcceleration.AddLabel (fsslow);
		lvAcceleration.AddLabel (fsnormal);
		lvAcceleration.AddLabel (fsfast);
		lvAcceleration.AddLabel (fsVeryfast);
    }

    /// <summary>
    /// Define regras fuzzy
    /// </summary>
    private void SetRules()
    {
   

		// Regras da Reta


		IS.NewRule("R1", "IF RoadSegment IS Reta And Frontal IS FarF  AND Left IS FarS THEN Angle IS VeryLittleNegative");
		IS.NewRule("R2", "IF RoadSegment IS Reta And Frontal IS FarF  AND Right IS FarS THEN Angle IS VeryLittlePositive");
		IS.NewRule("R3", "IF RoadSegment IS Reta And Frontal IS FarF THEN Angle IS Zero");
		IS.NewRule("R4", "IF RoadSegment IS Reta And Frontal IS MediumF AND Left Is FarS THEN Angle IS VeryLittleNegative");
		IS.NewRule("R5", "IF RoadSegment IS Reta And Frontal IS MediumF AND Right Is FarS THEN Angle IS VeryLittlePositive");
		IS.NewRule("R6", "IF RoadSegment IS Reta And Right Is NearS THEN Angle IS VeryLittleNegative");
		IS.NewRule("R7", "IF RoadSegment IS Reta And Frontal Is NearF AND Right IS FarS THEN Angle IS Positive");
		IS.NewRule("R8", "IF RoadSegment IS Reta And Frontal Is VeryNearF AND Right IS FarS THEN Angle IS Positive");


		// Regras da Curva


		IS.NewRule("R9", "IF RoadSegment IS Curva And CurvedFrontalLeft Is FarCF Then Angle IS VeryNegative");
		IS.NewRule("R10", "IF RoadSegment IS Curva And CurvedFrontalRight Is FarCF THEN Angle IS VeryPositive");
		IS.NewRule("R11", "IF RoadSegment IS Curva And RightFront Is FarS THEN Angle IS Positive");
		IS.NewRule("R12", "IF RoadSegment IS Curva And Right Is FarS THEN Angle IS Positive");
		IS.NewRule("R13", "IF RoadSegment IS Curva And RightRear Is FarS THEN Angle IS Positive");
		IS.NewRule("R14", "IF RoadSegment IS Curva And Frontal Is FarF THEN Angle IS Zero");
		IS.NewRule("R15", "IF RoadSegment IS Curva And FrontalRight Is FarF THEN Angle IS Zero");
		IS.NewRule("R16", "IF RoadSegment IS Curva And FrontalLeft Is FarF THEN Angle IS Zero");
		IS.NewRule("R17", "IF RoadSegment IS Curva And LeftFront Is FarS THEN Angle IS Negative");
		IS.NewRule("R18", "IF RoadSegment IS Curva And Left Is FarS THEN Angle IS Negative");
		IS.NewRule("R19", "IF RoadSegment IS Curva And LeftRear Is FarS THEN Angle IS Negative");
		IS.NewRule("R20", "IF RoadSegment IS Curva And CurvedFrontalLeft Is NearCF Then Angle IS VeryPositive");
		IS.NewRule("R21", "IF RoadSegment IS Curva And LeftFront Is FarS THEN Angle IS Negative");
		IS.NewRule("R22", "IF RoadSegment IS Curva And RightFront Is FarS THEN Angle IS Positive");
		IS.NewRule("R23", "IF RoadSegment IS Curva And LeftFront Is FarS AND Left IS FarS AND LeftRear IS FarS THEN Angle IS VeryNegative");
		IS.NewRule("R24", "IF RoadSegment IS Curva And RightFront Is FarS AND Right IS FarS AND RightRear IS FarS THEN Angle IS VeryPositive");
		IS.NewRule("R25", "IF RoadSegment IS Curva And CurvedFrontalRight Is NearCF Then Angle IS VeryNegative");


		//Regras RF


		IS.NewRule("R26", "IF RoadSegment IS Reta And RightFront IS FarS THEN Angle IS VeryLittlePositive");
		IS.NewRule("R27", "IF RoadSegment IS Reta And RightFront Is FarS THEN Angle IS VeryLittlePositive");
		IS.NewRule("R28", "IF RoadSegment IS Reta And RightFront Is MediumS THEN Angle IS Zero");


		//Regras RR


		IS.NewRule("R29", "IF RoadSegment IS Reta And RightRear IS FarS THEN Angle IS VeryLittlePositive");
		IS.NewRule("R30", "IF RoadSegment IS Reta And RightRear Is FarS THEN Angle IS VeryLittlePositive");
		IS.NewRule("R31", "IF RoadSegment IS Reta And RightRear Is MediumS THEN Angle IS Zero");


		//Regras LF


		IS.NewRule("R32", "IF RoadSegment IS Reta And LeftFront IS FarS THEN Angle IS VeryLittleNegative");
		IS.NewRule("R33", "IF RoadSegment IS Reta And LeftFront Is FarS THEN Angle IS VeryLittleNegative");
		IS.NewRule("R34", "IF RoadSegment IS Reta And LeftFront Is MediumS THEN Angle IS Zero");


		//Regras LR
		IS.NewRule("R35", "IF RoadSegment IS Reta And LeftRear IS FarS THEN Angle IS VeryLittleNegative");
		IS.NewRule("R36", "IF RoadSegment IS Reta And LeftRear Is FarS THEN Angle IS VeryLittleNegative");
		IS.NewRule("R37", "IF RoadSegment IS Reta And LeftRear Is MediumS THEN Angle IS Zero");


		// Regras Sensores Frontais


		IS.NewRule("R38", "IF RoadSegment IS Reta And FrontalRight Is NearF AND Right IS FarS THEN Angle IS Positive");
		IS.NewRule("R39", "IF RoadSegment IS Reta And FrontalRight Is VeryNearF AND Right IS FarS THEN Angle IS Positive");
		IS.NewRule("R40", "IF RoadSegment IS Reta And FrontalLeft Is NearF AND Right IS FarS THEN Angle IS Positive");
		IS.NewRule("R41", "IF RoadSegment IS Reta And FrontalLeft Is VeryNearF AND Right IS FarS THEN Angle IS Positive");
		IS.NewRule("R42", "IF RoadSegment IS Reta And CurvedFrontalLeft Is FarCF Then Angle IS Negative");
		IS.NewRule("R43", "IF RoadSegment IS Reta And CurvedFrontalRight Is FarCF THEN Angle IS Positive");


		// Aceleração


		IS.NewRule("R44", "IF RoadSegment IS Reta AND Frontal IS FarF AND FrontalRight IS FarF AND FrontalLeft IS FarF THEN Acceleration IS VeryFast");
		IS.NewRule("R45", "IF RoadSegment IS Reta AND Frontal IS MediumF AND FrontalRight IS MediumF AND FrontalLeft IS MediumF THEN Acceleration IS Fast");
		IS.NewRule("R46", "IF RoadSegment IS Reta AND CurvedFrontalRight IS NearCF THEN Acceleration IS Slow");
		IS.NewRule("R47", "IF RoadSegment IS Reta AND CurvedFrontalRight IS VeryNearCF THEN Acceleration IS Slow");
		IS.NewRule("R48", "IF RoadSegment IS Reta AND Frontal IS NearF AND FrontalRight IS NearF AND FrontalLeft IS NearF THEN Acceleration IS Stop");
		IS.NewRule("R49", "IF RoadSegment IS Reta AND Frontal IS NearF AND FrontalRight IS NearF AND FrontalLeft IS NearF THEN Acceleration IS Stop");
		IS.NewRule("R50", "IF CurvedFrontalLeft IS VeryNearCF THEN Acceleration IS Stop");
		IS.NewRule("R51", "IF CurvedFrontalRight IS VeryNearCF THEN Acceleration IS Stop");
		IS.NewRule("R52", "IF RoadSegment IS Reta AND Frontal IS FarF AND FrontalRight IS FarF AND FrontalLeft IS NOT FarF THEN Acceleration IS Normal");
		IS.NewRule("R53", "IF RoadSegment IS Reta AND Frontal IS FarF AND FrontalLeft IS FarF AND FrontalRight IS NOT FarF THEN Acceleration IS Normal");
		IS.NewRule("R54", "IF RoadSegment IS Curva AND Frontal IS NearF THEN Acceleration IS Slow");
		IS.NewRule("R55", "IF RoadSegment IS Curva AND Frontal IS MediumF THEN Acceleration IS Normal");
		IS.NewRule("R56", "IF RoadSegment IS Curva AND Frontal IS FarF THEN Acceleration IS Normal");
		IS.NewRule("R57", "IF RoadSegment IS Curva AND Frontal IS FarF AND FrontalRight IS FarF AND FrontalLeft IS FarF THEN Acceleration IS VeryFast");
		IS.NewRule("R58", "IF RoadSegment IS Curva THEN Acceleration IS Slow");
		IS.NewRule("R59", "IF RoadSegment IS Curva AND Frontal IS MediumF AND FrontalRight IS MediumF AND FrontalLeft IS MediumF THEN Acceleration IS Fast");
		IS.NewRule("R60", "IF RoadSegment IS Reta AND CurvedFrontalRight IS NOT NearCF AND CurvedFrontalRight IS NOT VeryNearCF AND CurvedFrontalLeft IS NOT NearCF AND CurvedFrontalLeft IS NOT VeryNearCF THEN Acceleration IS Fast");
		IS.NewRule("R61", "IF CurvedFrontalLeft IS VeryNearCF THEN Angle IS Positive");
		IS.NewRule("R62", "IF CurvedFrontalRight IS VeryNearCF THEN Angle IS Negative");


		// Carro adversario


		IS.NewRule("R63", "IF RoadSegment IS Reta And CarroAdversario IS Right THEN Angle IS Positive");
		IS.NewRule("R64", "IF RoadSegment IS Reta And CarroAdversario IS RightFront THEN Angle IS Positive");
		IS.NewRule("R65", "IF RoadSegment IS Reta And CarroAdversario IS RightRear THEN Angle IS Positive");
		IS.NewRule("R66", "IF RoadSegment IS Curva And CarroAdversario IS Right THEN Angle IS VeryPositive");
		IS.NewRule("R67", "IF RoadSegment IS Curva And CarroAdversario IS RightFront THEN Angle IS VeryPositive");
		IS.NewRule("R68", "IF RoadSegment IS Curva And CarroAdversario IS RightRear THEN Angle IS VeryPositive");
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Recupera o ângulo de acordo com as distancias frontal e lateral
    /// </summary>
	public float GetAngle(float frontal, float right, float left, float segment, float FrontRight, float RearRight, float FrontLeft, float RearLeft, float FrontalRight, float FrontalLeft, float CurvedFrontalRight, float CurvedFrontalLeft, float CarroAd)
    {
        IS.GetLinguisticVariable("Frontal").NumericInput = frontal;
		IS.GetLinguisticVariable("Right").NumericInput = right;
        IS.GetLinguisticVariable("Left").NumericInput = left;
		IS.GetLinguisticVariable ("RoadSegment").NumericInput = segment;
		IS.GetLinguisticVariable ("RightFront").NumericInput = FrontRight;
		IS.GetLinguisticVariable ("RightRear").NumericInput = RearRight;
		IS.GetLinguisticVariable ("LeftFront").NumericInput = FrontLeft;
		IS.GetLinguisticVariable ("LeftRear").NumericInput = RearLeft;
		IS.GetLinguisticVariable ("FrontalRight").NumericInput = FrontalRight;
		IS.GetLinguisticVariable ("FrontalLeft").NumericInput = FrontalLeft;
		IS.GetLinguisticVariable ("CurvedFrontalRight").NumericInput = CurvedFrontalRight;
		IS.GetLinguisticVariable ("CurvedFrontalLeft").NumericInput = CurvedFrontalLeft;
		IS.GetLinguisticVariable ("CarroAdversario").NumericInput = CarroAd;
        return IS.Evaluate("Angle");
    }

	public float GetAcceleration()
	{
		return IS.Evaluate ("Acceleration");
	}
    #endregion
}
