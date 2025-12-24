using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    //자원들
    private int Electricity; //전기
    private int Water; //물
    private int Money; //돈

    //수치들
    private int WaterSupplyRate; //물 공급량
    private int WasteDisposalRate; //쓰레기 처리량
    private int SecurityRate; //치안
    private int FireOccurrenceRate; //화재 발생률
    private int DiseaseIncidence; //질병 발생률
    private int UnEmploymentRate; //실업률
    private int Popularity; //인기도
    private int EducationRate; //교육률

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Electricity = 0;
        Water = 0;
        Money = 10000;
    }

    // Update is called once per frame티즌
    void Update()
    {
        
    }


    //자원량 조정
    //전기 자원량 조정
    public int UpdateElectricity(int Electricity)
    {
        //수치 조정
        this.Electricity += Electricity;
        return this.Electricity;
    }
    //물 자원량 조정
    public int UpdateWater(int Water)
    {
        //수치 조정
        this.Water += Water;
        return this.Water;
    }
    //돈 조정
    public int UpdateMoney(int Money)
    {
        //수치 조정
        this.Money += Money;
        return this.Money;
    }

    //수치들 조정
    //물 공급량 조정
    public int UpdateWaterSupplyRate(int WaterSupplyRate)
    {
        //수치 조정
        this.WaterSupplyRate += WaterSupplyRate;
        return this.WaterSupplyRate;
    }
    //쓰레기 처리량 조정
    public int UpdateWasteDisposalRate(int WasteDisposalRate)
    {
        //수치 조정
        this.WasteDisposalRate += WasteDisposalRate;
        return this.WasteDisposalRate;
    }
    //치안률 조정
    public int UpdateSecurityRate(int SecurityRate)
    {
        //수치 조정
        this.SecurityRate += SecurityRate;
        return this.SecurityRate;
    }
    //화재 발생률 조정
    public int UpdateFireOccurrenceRate(int FireOccurrenceRate)
    {
        //수치 조정
        this.FireOccurrenceRate += FireOccurrenceRate;
        return this.FireOccurrenceRate;
    }
    //질병 발생률 조정
    public int UpdateDiseaseIncidence(int DiseaseIncidence)
    {
        //수치 조정
        this.DiseaseIncidence += DiseaseIncidence;
        return this.DiseaseIncidence;
    }
    //실업률 조정
    public int UpdatUnEmploymentRate(int UnEmploymentRate)
    {
        //수치 조정
        this.UnEmploymentRate += UnEmploymentRate;
        return this.UnEmploymentRate;
    }
    //인기도 조정
    public int UpdatePopularity(int Popularity)
    {
        //수치 조정
        this.Popularity += Popularity;
        return this.Popularity;
    }
    //교육률 조정
    public int UpdateEducationRate(int EducationRate)
    {
        //수치 조정
        this.EducationRate += EducationRate;
        return this.EducationRate;
    }
}
