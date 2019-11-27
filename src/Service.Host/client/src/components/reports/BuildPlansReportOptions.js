import React, { useState, useEffect } from 'react';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import {
    Dropdown,
    Title,
    Loading,
    InputField,
    TypeaheadDialog,
    LinnWeekPicker
} from '@linn-it/linn-form-components-library';
import { makeStyles } from '@material-ui/styles';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

export default function BuildPlansReportOptions({
    history,
    buildPlans,
    cits,
    buildPlansLoading,
    citsLoading
}) {
    const [buildPlansOptions, setBuildPlansOptions] = useState([{ id: '', displayText: '' }]);
    const [citsOptions, setCitsOptions] = useState(['All']);

    const [reportOptions, setReportOptions] = useState({
        buildPlan: '',
        cit: 'All',
        weeks: 16
    });

    const weeksOptions = [16, 32, 48, 64];

    useEffect(() => {
        const list = ['All'];
        if (cits) {
            setCitsOptions([...list, ...cits.map(cit => cit.name)]);
            return;
        }
        setCitsOptions(list);
    }, [cits]);

    useEffect(() => {
        const list = [{ id: '', displayText: '' }];
        if (buildPlans) {
            setBuildPlansOptions([
                ...list,
                ...buildPlans.map(buildPlan => ({
                    id: buildPlan.buildPlanName,
                    displayText: `${buildPlan.buildPlanName} - ${buildPlan.description}`
                }))
            ]);
            return;
        }
        setBuildPlansOptions(list);
    }, [buildPlans]);

    const handleFieldChange = (propertyName, newValue) => {
        setReportOptions({ ...reportOptions, [propertyName]: newValue });
    };

    const handleRunClick = () => {
        console.log('RUN');
    };

    return (
        <Page>
            <Title text="Build Plans Report" />
            {/* TODO errors */}
            {buildPlansLoading || citsLoading ? (
                <Loading />
            ) : (
                <Grid style={{ marginTop: 40 }} container spacing={3} justify="center">
                    <Grid item xs={4}>
                        <Dropdown
                            label="Build Plan"
                            items={buildPlansOptions}
                            fullWidth
                            value={reportOptions.buildPlan}
                            onChange={handleFieldChange}
                            propertyName="buildPlan"
                        />
                    </Grid>
                    <Grid item xs={8} />
                    <Grid item xs={4}>
                        <Dropdown
                            label="Cit"
                            items={citsOptions}
                            fullWidth
                            value={reportOptions.cit}
                            onChange={handleFieldChange}
                            propertyName="cit"
                        />
                    </Grid>
                    <Grid item xs={8} />
                    <Grid item xs={4}>
                        <Dropdown
                            label="Weeks"
                            items={weeksOptions}
                            fullWidth
                            value={reportOptions.weeks}
                            onChange={handleFieldChange}
                            propertyName="weeks"
                        />
                    </Grid>
                    <Grid item xs={8} />
                    <Grid item xs={12}>
                        <Button
                            color="primary"
                            variant="contained"
                            style={{ float: 'right' }}
                            onClick={handleRunClick}
                        >
                            Run Report
                        </Button>
                    </Grid>
                </Grid>
            )}
        </Page>
    );
}
