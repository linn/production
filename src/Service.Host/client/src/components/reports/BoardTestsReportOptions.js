import React, { useState } from 'react';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import { DatePicker, Title, InputField } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Typography from '@material-ui/core/Typography';
import { makeStyles } from '@material-ui/styles';
import Page from '../../containers/Page';

function BoardTestsReportOptions({ history }) {
    const defaultStartDate = new Date();
    defaultStartDate.setMonth(defaultStartDate.getMonth() - 3);
    const [fromDate, setFromDate] = useState(defaultStartDate);
    const [toDate, setToDate] = useState(new Date());
    const [boardId, setBoardId] = useState('');

    const handleClick = () =>
        history.push({
            pathname: `/production/reports/board-tests-report/report`,
            search: `?fromDate=${fromDate.toISOString()}&toDate=${toDate.toISOString()}&boardId=${boardId}`
        });

    const useStyles = makeStyles(theme => ({
        marginTop: {
            marginTop: theme.spacing(3)
        }
    }));
    const classes = useStyles();

    const handleFieldChange = (_, newValue) => {
        setBoardId(newValue);
    };

    return (
        <Page>
            <Title text="Board Tests Report" />
            <Grid className={classes.marginTop} container spacing={3} justify="center">
                <Grid item xs={12}>
                    <Typography variant="h6" gutterBottom>
                        Choose a date range:
                    </Typography>
                </Grid>
                <Grid item xs={3}>
                    <DatePicker
                        label="From Date"
                        value={fromDate.toString()}
                        onChange={setFromDate}
                    />
                </Grid>
                <Grid item xs={3}>
                    <DatePicker
                        label="To Date"
                        value={toDate.toString()}
                        minDate={fromDate.toString()}
                        onChange={setToDate}
                    />
                </Grid>
                <Grid item xs={6} />
                <Grid item xs={3}>
                    <InputField
                        label="Board Id (Null for all)"
                        maxLength={14}
                        value={boardId}
                        onChange={handleFieldChange}
                        propertyName="boardId"
                    />
                </Grid>
                <Grid item xs={9} />
                <Grid item xs={12}>
                    <Button color="primary" variant="contained" onClick={handleClick}>
                        Run Report
                    </Button>
                </Grid>
            </Grid>
        </Page>
    );
}

BoardTestsReportOptions.propTypes = {
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    prevOptions: PropTypes.shape({
        fromDate: PropTypes.string,
        toDate: PropTypes.string
    }).isRequired
};

BoardTestsReportOptions.defaultProps = {};

export default BoardTestsReportOptions;
