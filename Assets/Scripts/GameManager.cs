using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Queue<Command> commands = new Queue<Command>();
    private Command currentCommand = null;

    [SerializeField] Transform cubeToMove;

    void Update()
    {
        ProcessCommand();
        ExecuteCommand();
    }

    private void ProcessCommand()
    {
        if (Input.GetMouseButtonDown(0)) AddMoveCommand();
        if (Input.GetMouseButtonDown(1)) AddScaleCommand();
    }


    private void AddMoveCommand()
    {
        Vector3 randomPos = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        MoveTheCube moveTheCube = new MoveTheCube(cubeToMove, randomPos);
        commands.Enqueue(moveTheCube);
    }

    private void AddScaleCommand()
    {
        float randomScale = Random.Range(0.1f, 5f);
        commands.Enqueue(new ScaleTheCube(cubeToMove, randomScale));
    }

    private void ExecuteCommand()
    {
        if (currentCommand != null && !currentCommand.IsFinished()) return; // Si une commande est en cours et n'est pas terminée, return.

        if (commands.Count == 0) return;    // Si la liste de commandes à exécuter est vide, return.

        currentCommand = commands.Dequeue();
        currentCommand.Execute();
    }
}


public abstract class Command
{
    public abstract void Execute();
    public abstract bool IsFinished();
}


public class MoveTheCube : Command
{
    private Transform cubeToMove;
    private Vector3 newPosition;

    public MoveTheCube(Transform cubeToMove, Vector3 newPosition)       // Constructeur
    {
        this.cubeToMove = cubeToMove;
        this.newPosition = newPosition;
    }

    public override void Execute()
    {
        cubeToMove.position = newPosition;
        // Si (par exemple) notre cube était un personnage sur Nav Mesh : agent.destination = newPosition;
    }

    public override bool IsFinished()
    {
        return true;    // Cette commande s'exécute instantanément, donc aussitôt créée, elle est déjà finie.
        // Si notre cube était un personnage sur un Nav Mesh : return agent.remainingDistance < 0.1f;
        // La commande ne sera donc pas terminée avant que le nav mesh agennt ait atteint sa destination.
    }
}


public class ScaleTheCube : Command
{
    private Transform cubeToScale;
    private float newScale;

    public ScaleTheCube(Transform cubeToMove, float newScale)       // Constructeur
    {
        this.cubeToScale = cubeToMove;
        this.newScale = newScale;
    }

    public override void Execute()
    {
        cubeToScale.localScale = new Vector3(newScale, newScale, newScale);               
    }

    public override bool IsFinished()
    {
        return true;    // Cette commande s'exécute instantanément, donc aussitôt créée, elle est déjà finie.        
    }
}
