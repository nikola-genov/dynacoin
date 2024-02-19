import React, { useState } from 'react';

const CoinsFileUpload = ({ onUploadComplete }) => {
    const [selectedFile, setSelectedFile] = useState(null);

    const handleFileChange = (event) => {
        setSelectedFile(event.target.files[0]);
    };

    const handleUpload = () => {
        const formData = new FormData();
        formData.append('file', selectedFile);

        // TODO - extract endpoint to external config
        fetch('coininfo/portfolio-summary-file', {
            method: 'POST',
            body: formData
        })
        .then(response => response.json())
        .then(data => {
            onUploadComplete(data);
        })
        .catch(error => {
            console.error('Error uploading coins file:', error);
        });
    };

    return (
        <div>
            <input type="file" onChange={handleFileChange} />
            <button onClick={handleUpload}>Upload Coins Data</button>
        </div>
    );
};

export default CoinsFileUpload;
