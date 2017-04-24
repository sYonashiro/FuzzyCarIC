using UnityEngine;
using System.Collections;
using AForge;
using AForge.Fuzzy;

public class Fuzzy
{
    #region Variáveis privadas
    private FuzzySet fsNearF, fsMediumF, fsFarF, fsNearS, fsMediumS, fsFarS,
    fsVeryNegative, fsNegative, fsZero, fsPositive, fsVeryPositive;

    private LinguisticVariable lvFrontal, lvLeft, lvRight, lvAngle;

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

        IS = new InferenceSystem(DB, new CentroidDefuzzifier(5));

        SetRules();
    }

    #region Métodos Privados
    /// <summary>
    /// Inicializa conjuntos fuzzy
    /// </summary>
    private void InitFuzzySets()
    {
        // Frontal
        fsNearF = new FuzzySet("NearF", new TrapezoidalFunction(60, 80, TrapezoidalFunction.EdgeType.Right));
        fsMediumF = new FuzzySet("MediumF", new TrapezoidalFunction(60, 80, 95, 110));
        fsFarF = new FuzzySet("FarF", new TrapezoidalFunction(95, 120, TrapezoidalFunction.EdgeType.Left));

        // Sides
        fsNearS = new FuzzySet("NearS", new TrapezoidalFunction(10, 15, TrapezoidalFunction.EdgeType.Right));
        fsMediumS = new FuzzySet("MediumS", new TrapezoidalFunction(20, 28, 34, 42));
        fsFarS = new FuzzySet("FarS", new TrapezoidalFunction(39, 46, TrapezoidalFunction.EdgeType.Left));

        // Angle
        fsVeryNegative = new FuzzySet("VeryNegative", new TrapezoidalFunction(-65, -25, TrapezoidalFunction.EdgeType.Right));
        fsNegative = new FuzzySet("Negative", new TrapezoidalFunction(-65, -25, -10, 0));
        fsZero = new FuzzySet("Zero", new TrapezoidalFunction(-10, 0, 10));
        fsPositive = new FuzzySet("Positive", new TrapezoidalFunction(0, 10, 25, 65));
        fsVeryPositive = new FuzzySet("VeryPositive", new TrapezoidalFunction(25, 65, TrapezoidalFunction.EdgeType.Left));
    }

    /// <summary>
    /// Inicializa variáveis linguísticas
    /// </summary>
    private void InitLinguisticVariables()
    {
        // Frontal
        lvFrontal = new LinguisticVariable("Frontal", 0, 1000);
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

        // Angle
        lvAngle = new LinguisticVariable("Angle", -90, 90);
        lvAngle.AddLabel(fsVeryNegative);
        lvAngle.AddLabel(fsNegative);
        lvAngle.AddLabel(fsZero);
        lvAngle.AddLabel(fsPositive);
        lvAngle.AddLabel(fsVeryPositive);
    }

    /// <summary>
    /// Define regras fuzzy
    /// </summary>
    private void SetRules()
    {
        IS.NewRule("R1", "IF Frontal IS FarF THEN Angle IS Zero");
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Recupera o ângulo de acordo com as distancias frontal e lateral
    /// </summary>
    public float GetAngle(float frontal, float left, float right)
    {
        IS.GetLinguisticVariable("Frontal").NumericInput = frontal;
        IS.GetLinguisticVariable("Left").NumericInput = right;
        IS.GetLinguisticVariable("Right").NumericInput = left;

        return IS.Evaluate("Angulo");
    }
    #endregion
}
