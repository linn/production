import React, { useState } from 'react';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import { DatePicker, Title } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';
import * as reportTypes from '../../reportTypes';

function ManufacturingCommitDateReportOptions({ history }) {
    const defaultDate = new Date();
    defaultDate.setDate(defaultDate.getDate() - 1);
    const [date, setDate] = useState(defaultDate);

    const handleClick = () =>
        history.push({
            pathname: `${reportTypes.manufacturingCommitDate.uri}`,
            search: `?date=${date.toISOString()}`
        });

    return (
        <Page>
            <Title text="Manufacturing Commit Date Report" />
            <Grid style={{ marginTop: 40 }} container spacing={3} justify="center">
                <Grid item xs={12}>
                    <Typography variant="h6" gutterBottom>
                        Choose a date:
                    </Typography>
                </Grid>
                <Grid item xs={3}>
                    <DatePicker label="Date" value={date} onChange={setDate} />
                </Grid>
                <Grid item xs={9} />
                <Grid item xs={12}>
                    <Button
                        color="primary"
                        variant="contained"
                        disabled={!date}
                        onClick={handleClick}
                    >
                        Run Report
                    </Button>
                </Grid>
            </Grid>
        </Page>
    );
}

ManufacturingCommitDateReportOptions.propTypes = {
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    prevOptions: PropTypes.shape({
        date: PropTypes.string
    }).isRequired
};

export default ManufacturingCommitDateReportOptions;
