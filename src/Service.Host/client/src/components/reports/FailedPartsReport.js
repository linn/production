import React, { useState } from 'react';
import {
    Loading,
    Title,
    MultiReportTable,
    InputField,
    Dropdown
} from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import PropTypes from 'prop-types';
import ExpansionPanel from '@material-ui/core/ExpansionPanel';
import ExpansionPanelSummary from '@material-ui/core/ExpansionPanelSummary';
import ExpansionPanelDetails from '@material-ui/core/ExpansionPanelDetails';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import Page from '../../containers/Page';

const Results = ({ reportData }) => (
    <>
        {reportData.length === 0 ? (
            <div>No results returned for selected parameters</div>
        ) : (
            <MultiReportTable
                reportData={reportData}
                showTotals
                placeholderRows={10}
                placeholderColumns={3}
                showRowTitles={false}
                showTitle
            />
        )}
    </>
);

function FailedPartsReport({ reportData, loading, fetchReport }) {
    const [parameters, setParameters] = useState({
        partNumber: '',
        orderByDate: '',
        excludeLinnProduced: 'false',
        vendorManager: '',
        stockPoolCode: ''
    });
    const handleFieldChange = (propertyName, newValue) => {
        setParameters({ ...parameters, [propertyName]: newValue });
    };

    return (
        <Page width="xl">
            <Grid container spacing={3} justify="center">
                <Grid item xs={12}>
                    <Title text="Failed parts" />
                </Grid>
                <Grid item xs={12}>
                    <ExpansionPanel>
                        <ExpansionPanelSummary
                            expandIcon={<ExpandMoreIcon />}
                            aria-controls="panel1a-content"
                            id="panel1a-header"
                        >
                            <Typography>Refine Search</Typography>
                        </ExpansionPanelSummary>
                        <ExpansionPanelDetails>
                            <Grid container spacing={3}>
                                <Grid item xs={3}>
                                    <InputField
                                        label="Part Number"
                                        value={parameters.partNumber}
                                        propertyName="partNumber"
                                        onChange={handleFieldChange}
                                        helperText="you can use * as a wildcard"
                                    />
                                </Grid>
                                <Grid item xs={3}>
                                    <Dropdown
                                        label="Order By Date?"
                                        value={parameters.orderByDate}
                                        items={[
                                            { id: 'ASC', displayText: 'oldest first' },
                                            { id: 'DESC', displayText: 'newest first' }
                                        ]}
                                        allowNoValue
                                        propertyName="orderByDate"
                                        helperText="leave blank to order by part"
                                        onChange={handleFieldChange}
                                    />
                                </Grid>
                                <Grid item xs={2}>
                                    <Dropdown
                                        label="Exclude Linn Produced"
                                        value={parameters.excludeLinnProduced}
                                        items={['false', 'true']}
                                        allowNoValue
                                        propertyName="excludeLinnProduced"
                                        onChange={handleFieldChange}
                                    />
                                </Grid>
                                <Grid item xs={2}>
                                    <InputField
                                        label="Vendor Manager"
                                        value={parameters.vendorManager}
                                        propertyName="vendorManager"
                                        onChange={handleFieldChange}
                                        helperText="T,S,A,C,M,E,B,R,K,D,I,L or O"
                                    />
                                </Grid>
                                <Grid item xs={2}>
                                    <InputField
                                        label="Stock Pool"
                                        value={parameters.stockPoolCode}
                                        propertyName="stockPoolCode"
                                        onChange={handleFieldChange}
                                        helperText="stock pool code e.g. LINN, REWORK"
                                    />
                                </Grid>
                                <Grid item xs={2}>
                                    <Button
                                        variant="outlined"
                                        onClick={() =>
                                            setParameters({
                                                partNumber: '',
                                                orderByDate: ''
                                            })
                                        }
                                    >
                                        Clear
                                    </Button>
                                </Grid>
                                <Grid item xs={2}>
                                    <Button
                                        variant="outlined"
                                        color="primary"
                                        disabled={Object.values(parameters).every(v => !v)}
                                        onClick={() => fetchReport(parameters)}
                                    >
                                        Go
                                    </Button>
                                </Grid>
                            </Grid>
                        </ExpansionPanelDetails>
                    </ExpansionPanel>
                </Grid>
                <Grid item xs={12}>
                    {loading || !reportData ? <Loading /> : <Results reportData={reportData} />}
                </Grid>
            </Grid>
        </Page>
    );
}

Results.propTypes = {
    reportData: PropTypes.arrayOf(PropTypes.shape({}))
};

Results.defaultProps = {
    reportData: []
};

FailedPartsReport.propTypes = {
    reportData: PropTypes.arrayOf(PropTypes.shape({})),
    loading: PropTypes.bool,
    options: PropTypes.shape({}),
    fetchReport: PropTypes.func.isRequired
};

FailedPartsReport.defaultProps = {
    reportData: [],
    options: {},
    loading: false
};

export default FailedPartsReport;
