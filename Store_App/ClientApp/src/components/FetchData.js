import React, { useState, useEffect } from 'react';

export function FetchData() {
    const [forecasts, setForecasts] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const populateWeatherData = async () => {
            try {
                const headers = {'Authorization': "Bearer eyJhbGciOiJBMjU2S1ciLCJlbmMiOiJBMjU2Q0JDLUhTNTEyIiwidHlwIjoiSldUIiwiY3R5IjoiSldUIn0.OQJkOjlRjDV2sIseLeutWEG6y2I2BFxbgHxQPTfFxwkgV0F62G1HLt54V-c_FZmuqF7q77my0xNqu_fJL5m7E1AbxN-BSBfc.WLe_atCwbSdLd-e4CGFOQg.Xwjya_lbouvaKgJGWgdGbc2B-ch1LR_R7vE0yVstqnTKvVlQ4PHqUoqY6n_iu6B2phix5qhkReFcF1bYVmpo32f0Ixkm7P8BO9WnkIHVcSc-T17Ru-TVtUkzuV7M-oSJQh7lrfIxQbDJn8fJaQML0osrAX_ADpKKmUinnmJqCtqE5IxKYnrwibzA8oqicHiz-Z3A3rD1XFezI08JhdjJ81YSErRCl9n_8Z7jqK1vKm2nMX8J1YffAybJhJtc6UbDxIvkxt_ol0-bd8eEo5jLjf5ZWf8OGiyN5WlXXqT7dS4_mh8ZUBHj2AYEgj3VBMgdLU229whV6dYx07ccUDhjgi34WaQA3OIvWM0eaSN0-t8KF-TQFr_UkWbh8sFnIl98rZiDYHj6kcE-ZCihjpYWKg.R80208VHPr0idlUBylYV33xlAN72oBmoqlDPZwAthpo"};
                const response = await fetch(`api/weatherforecast/getforecast`, { headers });
                const data = await response.json();
                setForecasts(data);
                setLoading(false);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        populateWeatherData();
    }, []);

    const renderForecastsTable = (forecasts) => {
        return (
            <table className="table table-striped" aria-labelledby="tableLabel">
                <thead>
                    <tr>
                        <th>Date</th>
                        <th>Temp. (C)</th>
                        <th>Temp. (F)</th>
                        <th>Summary</th>
                    </tr>
                </thead>
                <tbody>
                    {forecasts.map((forecast) => (
                        <tr key={forecast.date}>
                            <td>{forecast.date}</td>
                            <td>{forecast.temperatureC}</td>
                            <td>{forecast.temperatureF}</td>
                            <td>{forecast.summary}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        );
    };

    const contents = loading ? (
        <p>
            <em>Loading...</em>
        </p>
    ) : (
        renderForecastsTable(forecasts)
    );

    return (
        <div>
            <h1 id="tableLabel">Weather forecast</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>
    );
}

