import React, { useState } from 'react';
import PropTypes from 'prop-types';
import moment from 'moment';
import {
    Title,
    InputField,
    SaveBackCancelButtons,
    Loading
} from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import Page from '../../containers/Page';

export default function CreateBuildPlan({ loading, history, saveBuildPlan }) {
    const [buildPlan, setBuildPlan] = useState({});

    const handleFieldChange = (propertyName, newValue) => {
        setBuildPlan({ ...buildPlan, [propertyName]: newValue });
    };

    const handleSaveClick = () => {
        saveBuildPlan({ ...buildPlan, dateCreated: moment().toISOString() });
    };

    const handleBackClick = () => {
        history.goBack();
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Create Build Plan" />
                </Grid>
                {loading ? (
                    <Loading />
                ) : (
                    <>
                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                value={buildPlan.buildPlanName}
                                label="Build Plan Name"
                                onChange={handleFieldChange}
                                propertyName="buildPlanName"
                                required
                            />
                        </Grid>
                        <Grid item xs={8} />
                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                value={buildPlan.description}
                                label="Description"
                                onChange={handleFieldChange}
                                propertyName="description"
                                maxLength={50}
                                required
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <SaveBackCancelButtons
                                saveDisabled={!buildPlan.description && !buildPlan.buildPlanName}
                                saveClick={handleSaveClick}
                                cancelClick={handleBackClick}
                                backClick={handleBackClick}
                            />
                        </Grid>
                    </>
                )}
            </Grid>
        </Page>
    );
}

CreateBuildPlan.propTypes = {
    history: PropTypes.shape({ goBack: PropTypes.func }).isRequired,
    loading: PropTypes.bool,
    saveBuildPlan: PropTypes.func.isRequired
};

CreateBuildPlan.defaultProps = {
    loading: false
};
