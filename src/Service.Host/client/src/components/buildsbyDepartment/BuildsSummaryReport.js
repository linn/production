import React, { Fragment, useState } from 'react';
import Grid from '@material-ui/core/Grid';
import {
    ReportTable,
    Loading,
    Title,
    BackButton,
    ErrorCard
} from '@linn-it/linn-form-components-library';
import { makeStyles } from '@material-ui/core/styles';
import Stepper from '@material-ui/core/Stepper';
import Step from '@material-ui/core/Step';
import StepLabel from '@material-ui/core/StepLabel';
import StepContent from '@material-ui/core/StepContent';
import Typography from '@material-ui/core/Typography';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

const useStyles = makeStyles(theme => ({
    root: {
        width: '90%'
    },
    button: {
        marginTop: theme.spacing(1),
        marginRight: theme.spacing(1)
    },
    actionsContainer: {
        marginBottom: theme.spacing(2)
    },
    resetContainer: {
        padding: theme.spacing(3)
    }
}));

function Report({ reportData, loading }) {
    const getSteps = () =>
        reportData ? reportData.map(r => r.reportResults[0].title.displayString) : [''];

    const classes = useStyles();
    const [activeStep, setActiveStep] = useState(false);

    const handleStep = step => () => {
        if (activeStep === step) {
            setActiveStep(false);
        } else {
            setActiveStep(step);
        }
    };

    const getStepContent = index => (
        <Fragment>
            {reportData && reportData[index].reportResults[0].results.length > 0 ? (
                <ReportTable
                    showRowTitles={false}
                    reportData={reportData[index].reportResults[0]}
                    showTotals
                    title={
                        reportData ? reportData[index].reportResults[0].title.displayString : null
                    }
                    showTitle={false}
                />
            ) : (
                <ErrorCard errorMessage="No Data for specified range." />
            )}
        </Fragment>
    );

    if (loading) {
        return <Loading />;
    }
    return (
        <div className={classes.root}>
            <Stepper activeStep={activeStep} orientation="vertical" nonLinear>
                {getSteps().map((label, index) => (
                    <Step key={label} onClick={handleStep(index)}>
                        <StepLabel>{label}</StepLabel>
                        <StepContent>
                            <Typography>{getStepContent(index)}</Typography>
                        </StepContent>
                    </Step>
                ))}
            </Stepper>
        </div>
    );
}

function BuildsSummaryReport({ reportData, loading, history, errorMessage }) {
    const handleBackClick = () => {
        history.push('/production/reports/builds-summary/options');
    };
    return (
        <Page>
            <Grid container spacing={3} justify="center">
                {errorMessage && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={errorMessage} />
                    </Grid>
                )}
                <Grid item xs={12}>
                    <Grid item xs={10}>
                        <Title
                            text={
                                loading || !reportData
                                    ? 'Crunching the numbers...'
                                    : `Builds Summary`
                            }
                        />
                    </Grid>
                </Grid>
                <Grid item xs={12}>
                    <Report reportData={reportData} loading={loading} />
                </Grid>
                <Grid item xs={12}>
                    <BackButton backClick={handleBackClick} />
                </Grid>
            </Grid>
        </Page>
    );
}

BuildsSummaryReport.propTypes = {
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    reportData: PropTypes.shape({}),
    loading: PropTypes.bool,
    errorMessage: PropTypes.string
};

Report.propTypes = {
    reportData: PropTypes.shape({}),
    loading: PropTypes.bool
};

Report.defaultProps = {
    loading: false,
    reportData: {}
};

BuildsSummaryReport.defaultProps = {
    reportData: null,
    loading: false,
    errorMessage: ''
};

export default BuildsSummaryReport;
