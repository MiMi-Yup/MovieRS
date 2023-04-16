using Microsoft.ML;
using Microsoft.ML.Data;

const string DATASET = "./dataset";

MLContext mlContext = new MLContext();

IDataView dataset = LoadData(mlContext);
DataOperationsCatalog.TrainTestData trainTestData = mlContext.Data.TrainTestSplit(dataset, testFraction: 0.1);
ITransformer model = BuildAndTrailModel(mlContext, trainTestData.TrainSet);

EvaluateModel(mlContext, trainTestData.TestSet, model);
UseModelForSinglePrediction(mlContext, model);
SaveModel(mlContext, trainTestData.TrainSet.Schema, model);

void SaveModel(MLContext mlContext, DataViewSchema trainingDataViewSchema, ITransformer model)
{
    var modelPath = "./dataset/model.zip";

    Console.WriteLine("=============== Saving the model to a file ===============");
    mlContext.Model.Save(model, trainingDataViewSchema, modelPath);
}

void UseModelForSinglePrediction(MLContext mlContext, ITransformer model)
{
    Console.WriteLine("=============== Making a prediction ===============");
    var predictionEngine = mlContext.Model.CreatePredictionEngine<MovieRating, MovieRatingPrediction>(model);
    var testInput = new MovieRating { userId = 6, movieId = 10 };

    var movieRatingPrediction = predictionEngine.Predict(testInput);
    if (Math.Round(movieRatingPrediction.Score, 1) > 3.5)
    {
        Console.WriteLine("Movie " + testInput.movieId + " is recommended for user " + testInput.userId);
    }
    else
    {
        Console.WriteLine("Movie " + testInput.movieId + " is not recommended for user " + testInput.userId);
    }
}

void EvaluateModel(MLContext mlContext, IDataView testDataView, ITransformer model)
{
    Console.WriteLine("=============== Evaluating the model ===============");
    var prediction = model.Transform(testDataView);
    var metrics = mlContext.Regression.Evaluate(prediction, labelColumnName: "Label", scoreColumnName: "Score");
    Console.WriteLine("Root Mean Squared Error : " + metrics.RootMeanSquaredError.ToString());
    Console.WriteLine("RSquared: " + metrics.RSquared.ToString());
}

ITransformer BuildAndTrailModel(MLContext mlContext, IDataView trainingData)
{
    IEstimator<ITransformer> estimator = mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "userIdEncoded", inputColumnName: "userId")
        .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "movieIdEncoded", inputColumnName: "movieId"));
    var options = new Microsoft.ML.Trainers.MatrixFactorizationTrainer.Options
    {
        MatrixColumnIndexColumnName = "userIdEncoded",
        MatrixRowIndexColumnName = "movieIdEncoded",
        LabelColumnName = "Label",
        NumberOfIterations = 30,
        ApproximationRank = 100,
    };
    var trainerEstimator = estimator.Append(mlContext.Recommendation().Trainers.MatrixFactorization(options));
    Console.WriteLine("=============== Training the model ===============");
    return trainerEstimator.Fit(trainingData);
}

IDataView LoadData(MLContext mlContext)
{
    string dataset = Path.GetFullPath(Path.Combine(DATASET, "ratings.csv"));
    return mlContext.Data.LoadFromTextFile<MovieRating>(dataset, hasHeader: true, separatorChar: ',', allowQuoting: true);
}

public class MovieRating
{
    [LoadColumn(0)]
    public float userId;
    [LoadColumn(1)]
    public float movieId;
    [LoadColumn(2)]
    public float Label;
}

public class MovieRatingPrediction
{
    public float Label;
    public float Score;
}