using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Web;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

public class OntologyReader : MonoBehaviour {

    private JSONNode result;
    private string user = "jspirig";
    private string passcode = "MxkA3dCcFE7";
    private string ontologyEndpoint;

    private string selectThingsRq = @"PREFIX td: <https://www.w3.org/2019/wot/td#>
PREFIX htv: <http://www.w3.org/2011/http#>
PREFIX hctl: <https://www.w3.org/2019/wot/hypermedia#>
PREFIX dc: <http://purl.org/dc/elements/1.1/>
PREFIX js: <https://www.w3.org/2019/wot/json-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX bot: <https://w3id.org/bot#>
PREFIX xsd: <http://www.w3.org/2001/XMLSchema#>
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
SELECT DISTINCT  ?thing ?method  ?target ?affordance ?affordanceName ?spaceName
WHERE {
    ?td a td:Thing .
    ?td dc:title ?thing .
    ?td td:hasInteractionAffordance ?aff .
    ?aff td:hasForm ?form .
    ?aff rdfs:comment ?affordance .
    ?form htv:methodName ?method .
    ?form hctl:forContentType ?contentType .
    ?form hctl:hasOperationType ?operationType .
    ?form hctl:hasTarget ?target .
    OPTIONAL {
        ?space a bot:Space .
        ?aff td:name ?affordanceName .
        ?space dc:title ?spaceName .
        ?space bot:containsElement ?td .
        # Retrieve json schema to build payload
        # ?affordance td:hasInputSchema ?schema .
        # ?schema js:properties ?property .
        # ?property a ?propertyType .
        # ?property js:propertyName ?propertyName .
    }
}";
    private string selectThreshRq = @"PREFIX ex: <http://www.purl.org/oema/externalfactors/CarbonDioxide>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX units: <http://www.purl.org/oema/units/>
PREFIX dCompanion: <https://things.interactions.ics.unisg.ch/dc#>
PREFIX dc: <http://purl.org/dc/terms/>
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
SELECT DISTINCT ?desc ?value ?unitLabel WHERE { 
?pollutant a <http://www.purl.org/oema/externalfactors/CarbonDioxide> .
?pollutant a <http://www.purl.org/oema/externalfactors/PollutantTargetValue> .
?pollutant units:unitOfMeasure ?unit .
?unit rdfs:label ?unitLabel .
?pollutant dCompanion:value ?value .
?pollutant rdfs:comment ?desc .
}";

    public List<Endpoint> endpoints = new List<Endpoint>();
    public List<Threshold> thresholds = new List<Threshold>();
    public bool endpointsSet;
    public bool thresholdsSet;

    void Awake() {
        ontologyEndpoint =  $"https://{user}:{passcode}@graphdb.interactions.ics.unisg.ch/repositories/dc?query=";
        endpointsSet = false;
        thresholdsSet = false;
        // query and set endpoints
        StartCoroutine(queryOntology(encodeQuery(selectThingsRq), "endpoints")); 
        StartCoroutine(queryOntology(encodeQuery(selectThreshRq), "thresholds"));
    }

    private string encodeQuery(string value)  
    {  
        StringBuilder retval = new StringBuilder();  
        foreach (char c in value)  
        {   
            if ((c >= 48 && c <= 57) || //0-9  
                (c >= 65 && c <= 90) || //a-z  
                (c >= 97 && c <= 122) || //A-Z                    
                (c == 45 || c == 46 || c == 95 || c == 126)) // period, hyphen, underscore, tilde  
            {  
                retval.Append(c);  
            }  
            else  
            {  
                retval.AppendFormat("%{0:X2}", ((byte)c));  
            }  
        }

        string resultString = retval.ToString().Replace("%20", "+");

        return resultString;
    }  

    public IEnumerator queryOntology(string query, string what) {

        var uri = ontologyEndpoint + query;
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        uwr.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        yield return uwr.SendWebRequest();

        /*
        // JSON.Parse(uwr.downloadHandler.text);
        Debug.Log(uwr.downloadHandler.text);
        var csvTable = new DataTable();  
                using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(@"D:\CSVFolder\CSVFile.csv")), true))  
                {  
                    csvTable.Load(csvReader);  
                } 
        */

        string response = uwr.downloadHandler.text;
        string[] lines = Regex.Split(response, Environment.NewLine);
        
        if (what == "endpoints") {
            for (int i = 1; i <= lines.Length - 2; i++) {
                
                string[] line = lines[i].Split(Convert.ToChar(","));
            

                Endpoint endpoint = new Endpoint((string)line[0], (string)line[1], (string)line[2], (string)line[3], (string)line[4], (string)line[5]);
                endpoints.Add(endpoint);
            }
            endpointsSet = true;
        }
        if (what == "thresholds") {

            for (int i = 1; i <= lines.Length - 2; i++) {
                string[] line = lines[i].Split(Convert.ToChar(","));
                Threshold threshold = new Threshold((string)line[0], Convert.ToInt32(line[1]), (string)line[2]);
                thresholds.Add(threshold);
            }
            thresholdsSet = true;
        }
    }
}


public class Endpoint {
    public string thing;
    public string method;
    public string uri;
    public string actionName;
    public string actionDescription;
    public string space;

    public Endpoint(string thing, string method, string uri, string actionName, string actionDescription, string space) {
        this.thing = thing;
        this.method = method;
        this.uri = uri;
        this.actionName = actionName;
        this.actionDescription = actionDescription;
        this.space = space;
    }   
}


public class Threshold {
    public string desc;
    public int value;
    public string unit;

    public Threshold(string desc, int value, string unit) {
        this.desc = desc;
        this.value = value;
        this.unit = unit;
    }   
}