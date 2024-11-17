window.addEventListener('DOMContentLoaded', (event)=>{
    getVisitCount();
})

const functionApiUrl ='https://getresumecountervalue.azurewebsites.net/api/GetResumeCounter?code=ilJf3oY_YHlqdlaBaEB_WeTTdCH-uOe4Tpf-jBJCphXsAzFuXdzMow%3D%3D';
const LocalFunctionApi = 'http://localhost:7071/api/GetResumeCounter';

const getVisitCount = () => {
    let count =30;
    fetch(functionApiUrl).then(response => {
        return response.json()
    }).then(response =>{
        console.log("Website called function API.");
        count = response.Count;
        document.getElementById('counter').innerText = count;
    }) .catch(function(error){
        console.log(error);
    });
    return count;
}