import React, { useState } from 'react';
import { WeatherForecastComponentWrapper, GetWeatherBtn, WeatherCard, WeatherCardGrid, Temperature, DateText, SummaryText } from './WeatherForecastComponent.styled.ts';
import axios from 'axios';

const WeatherForecastComponent = () => {
   const [weather, setWeather] = useState<any[]>([]);

   async function handleSubmit(event: React.MouseEvent<HTMLButtonElement>) {
      event.preventDefault();

      const token = localStorage.getItem("token");
      try {
         const response = await axios.get("https://localhost:7231/WeatherForecast", {
            headers: {
               Authorization: `Bearer ${token}`
            }
         });

         setWeather(response.data);
      } catch (error) {
         console.error("Error!!!", error);
      }
   }

   return (
      <WeatherForecastComponentWrapper>
         <WeatherCardGrid>
            {weather.map((forecast, index) => (
               <WeatherCard key={index}>
                  <DateText>{new Date(forecast.date).toLocaleString()}</DateText>
                  <SummaryText>{forecast.summary}</SummaryText>
                  <Temperature>Temperature: <br />{forecast.temperatureC} °C <br />{forecast.temperatureF} °F</Temperature>
               </WeatherCard>
            ))}
         </WeatherCardGrid>
         <GetWeatherBtn onClick={handleSubmit}>Get Weather</GetWeatherBtn>
      </WeatherForecastComponentWrapper>
   );
};

export default WeatherForecastComponent;
