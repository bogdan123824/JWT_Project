import styled from 'styled-components';

export const WeatherForecastComponentWrapper = styled.div`
   margin: 20px;
`;

export const GetWeatherBtn = styled.button`
   color: black;
   margin: 20px auto;
   display: block;
   height: 30px;
   width: 120px;
   background-color: #e0e0e0;
   border: 1px solid #ccc;
   border-radius: 5px;
`;

export const WeatherCardGrid = styled.div`
   display: flex;
   flex-wrap: wrap;
   justify-content: space-around;
`;

export const WeatherCard = styled.div`
   background-color: white;
   border: 1px solid #ccc;
   border-radius: 10px;
   width: 200px;
   padding: 15px;
   margin: 10px;
   box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
   text-align: center;
`;

export const DateText = styled.div`
   font-size: 14px;
   margin-bottom: 10px;
   color: #555;
`;

export const SummaryText = styled.div`
   font-size: 16px;
   font-weight: bold;
   margin-bottom: 15px;
   color: #333;
`;

export const Temperature = styled.div`
   font-size: 18px;
   color: #000;
`;
