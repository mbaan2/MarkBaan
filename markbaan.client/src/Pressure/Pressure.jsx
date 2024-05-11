import React, { useState, useEffect } from 'react';
import axios from 'axios';

const Pressure = () => {
    const [pressure, setPressure] = useState(0);

    useEffect(() => {
        const intervalId = setInterval(() => {
            axios.get('/api/reactor/getpressure')
                .then(response => {
                    setPressure(response.data);
                });
        }, 500);

    return () => clearInterval(intervalId);
    }, []);

    const getPressureColor = (pressureValue) => {
        if (pressureValue < 0.60) return 'green';
        if (pressureValue >= 0.60 && pressureValue < 0.85) return 'darkorange';
        if (pressureValue >= 0.85) return 'red';
    }

    return (
        <>
            <h2 tabIndex="0">Current Pressure: 
                <span tabIndex="0" id="pressureValue" style={{ color: getPressureColor(pressure) }}>
                    {pressure.toFixed(2)}
                </span>
            </h2>
        </>
    );
};

export default Pressure;
