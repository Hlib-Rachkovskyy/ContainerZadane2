namespace ContainerZadanie2;

public abstract class Kontener
{
    private char typKontenera { get; }
    private double masaLadunkuKG { get; set; }
    private double wysokoscCM { get; }
    private double wlasnaWagaKG { get;  }
    private double glebokoscCM { get;  }
    private static int id = 1; 
    private string numerSeryjny { get; }
    private double maxLadownoscKG { get; }

    public Kontener(char typKontenera ,double masaLadunkuKG, double wysokoscCM, double wlasnaWagaKG, double glebokoscCM, double maxLadownoscKG )
    {
        this.typKontenera = typKontenera;
        this.masaLadunkuKG = masaLadunkuKG;
        this.wysokoscCM = wysokoscCM;
        this.wlasnaWagaKG = wlasnaWagaKG;
        this.glebokoscCM = glebokoscCM;
        this.maxLadownoscKG = maxLadownoscKG;
        numerSeryjny = "KON-" + typKontenera +  "-" + id;
        id++;
    }

    public virtual void Load(double waga)
    {
        if (masaLadunkuKG > maxLadownoscKG)
        {
            throw new OverfillException(numerSeryjny);
        }

        masaLadunkuKG += waga;
    }
    public abstract void UnLoad();
    
}


class KonteneryNaPlyny : Kontener, IHazardNotifier

{
    private string typLadunku;
    public KonteneryNaPlyny(char typKontenera ,double masaLadunkuKG, double wysokoscCM, double wlasnaWagaKG, double glebokoscCM, double maxLadownoscKG, string typLadunku) : base('L', masaLadunkuKG, wysokoscCM, wlasnaWagaKG, glebokoscCM, maxLadownoscKG)
    {
        this.typLadunku = typLadunku;
    }

    public override void Load(double waga)
    {
        if (typLadunku.Equals("Niebezpieczny"))
        {
            base.Load(waga/2);

        }
    }

    public override void UnLoad()
    {
        throw new NotImplementedException();
    }

    public void NotifyDanger(string containerNumber)
    {
        throw new NotImplementedException();
    }
}

internal interface IHazardNotifier
{
    void NotifyDanger(string containerNumber);

}

public class OverfillException : Exception
{
    public override string Message
    {
        get
        {
            return "The container is overfilled!";
        }
    }

    public OverfillException(string containerNumber) 
        : base($"The container with number {containerNumber} is overfilled.")
    {
    }
}
