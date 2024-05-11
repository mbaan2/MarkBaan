import React, { useState, useLayoutEffect } from 'react';
import axios from 'axios';
import './ReactorControls.css'

const ReactorControls = () => {
    const [currentIncreasePercentage, setCurrentIncreasePercentage] = useState(0.2);
    const [currentReleasePercentage, setCurrentReleasePercentage] = useState(0.2);

    useLayoutEffect(() => {
        axios.get('/api/reactor/GetPressureValues')
            .then(response => {
                setCurrentIncreasePercentage(Number(response.data.increasePressure));
                setCurrentReleasePercentage(Number(response.data.releasePressure));
            });
    }, []);

    function updateReactorValues(event) {
        event.preventDefault();

        const alert = document.getElementById("alertForm");
        resetAlert(alert)

        if (currentIncreasePercentage >= currentReleasePercentage) {
            alert.classList.add("error");
            alert.innerText = "Increase pressure should be lower than release pressure!"
            return;
        }

        let PressureModel = {
            IncreasePressure: currentIncreasePercentage,
            ReleasePressure: currentReleasePercentage
        };

        axios.put('/api/reactor/AdjustPressureValues', PressureModel)
            .then(response => {
                alert.innerText = response.data;
            })
            .catch(error => {
                alert.classList.add("error");
                alert.innerText = error.response.data;
            });
    }
    const resetAlert = (alert) => {
        alert.innerText = "";
        if (alert.classList.contains("error")) alert.classList.remove("error");
    }

    const handleIncreasePressureChange = (event) => setCurrentIncreasePercentage(event.target.value);
    const handleReleasePressureChange = (event) => setCurrentReleasePercentage(event.target.value);

    return (
        <form onSubmit={updateReactorValues}>
            <label htmlFor="increasePercentageId">
                When to increase pressure:
                <input
                    id="increasePercentageId"
                    type="number"
                    min="0.2"
                    max="0.85"
                    step="0.01"
                    value={currentIncreasePercentage}
                    name="increasePercentage"
                    onChange={handleIncreasePressureChange}
                    required
                />
            </label>
            <label htmlFor="releasePressureId">
                When to release pressure:
                <input
                    id="releasePressureId"
                    type="number"
                    min="0.2"
                    max="0.85"
                    step="0.01"
                    value={currentReleasePercentage}
                    name="releasePressure"
                    onChange={handleReleasePressureChange}
                    required
                />
            </label>
            <button type="submit" aria-label="Submit form to update pressure values">Submit</button>
            <p id="alertForm" aria-live="assertive" role="alert"></p>

        </form>
    );
};

export default ReactorControls;
