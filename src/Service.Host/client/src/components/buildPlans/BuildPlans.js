import React, { Fragment, useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import {
    Title,
    Loading,
    InputField,
    Dropdown,
    EditableTable,
    DatePicker,
    CreateButton,
    SaveBackCancelButtons,
    ErrorCard,
    SnackbarMessage
} from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import Page from '../../containers/Page';

export default function BuildPlans({
    itemErrors,
    buildPlans,
    buildPlansLoading,
    buildPlanDetails,
    buildPlanDetailsLoading,
    buildPlanRules,
    buildPlanRulesLoading,
    partsSearchResults,
    partsSearchLoading,
    searchParts,
    clearPartsSearch,
    selectedBuildPlan,
    updateBuildPlan,
    history,
    updateBuildPlanDetail,
    saveBuildPlanDetail,
    buildPlanDetailLoading,
    buildPlanDetail,
    fetchBuildPlanDetails,
    fetchBuildPlans,
    fetchBuildPlanRules,
    buildPlanSnackbarVisible,
    buildPlanDetailSnackbarVisible,
    setBuildPlanSnackbarVisible,
    setBuildPlanDetailSnackbarVisible
}) {
    const [buildPlan, setBuildPlan] = useState({ buildPlanName: '', description: '' });
    const [buildPlanOptions, setBuildPlanOptions] = useState([{ id: '', displayText: '' }]);
    const [buildPlanDetailOptions, setBuildPlanDetailOptions] = useState([]);
    const [buildPlanRuleOptions, setBuildPlanRuleOptions] = useState([]);
    const [newRow, setNewRow] = useState({});
    const [editing, setEditing] = useState(false);

    useEffect(() => {
        if (buildPlanDetail) {
            fetchBuildPlanDetails();
            fetchBuildPlans();
            fetchBuildPlanRules();
        }
    }, [buildPlanDetail, fetchBuildPlanDetails, fetchBuildPlans, fetchBuildPlanRules]);

    useEffect(() => {
        if (buildPlans) {
            const options = [{ id: '', displayText: '' }];

            setBuildPlanOptions([
                ...options,
                ...buildPlans.map(bp => ({
                    ...bp,
                    id: bp.buildPlanName,
                    displayText: bp.buildPlanName
                }))
            ]);
        }
    }, [buildPlans]);

    useEffect(() => {
        if (buildPlans && selectedBuildPlan) {
            setBuildPlan(buildPlanOptions.find(bp => bp.buildPlanName === selectedBuildPlan));
        }
    }, [buildPlans, selectedBuildPlan, buildPlanOptions]);

    useEffect(() => {
        if (buildPlanDetails) {
            setBuildPlanDetailOptions(
                buildPlanDetails.map(bpd => ({
                    ...bpd,
                    id: `${bpd.partNumber}${bpd.buildPlanName}${bpd.fromDate}`
                }))
            );
        }
    }, [buildPlanDetails]);

    useEffect(() => {
        if (buildPlanRules) {
            setBuildPlanRuleOptions(
                buildPlanRules.map(bpr => ({
                    ...bpr,
                    id: bpr.ruleCode,
                    displayText: bpr.description
                }))
            );
        }
    }, [buildPlanRules]);

    const selectPartSearchResult = (_propertyName, part, updatedItem) => {
        if (updatedItem.id) {
            setBuildPlanDetailOptions(
                buildPlanDetailOptions.map(bpd =>
                    bpd.id === updatedItem.id
                        ? { ...bpd, partNumber: part.partNumber, partDescription: part.description }
                        : bpd
                )
            );
        }

        setNewRow(() => ({
            ...updatedItem,
            partNumber: part.partNumber,
            partDescription: part.description,
            buildPlanName: buildPlan.buildPlanName
        }));
    };

    const handleSaveClick = () => {
        updateBuildPlan(buildPlan);
    };

    const handleCancelClick = () => {
        setBuildPlan({ buildPlanName: '', description: '' });
    };

    const handleBackClick = () => {
        history.goBack();
    };

    const handleUpdateBuildPlanDetail = updatedBuildPlanDetail => {
        updateBuildPlanDetail(null, updatedBuildPlanDetail);
    };

    const handleSaveBuildPlanDetail = updatedBuildPlanDetail => {
        saveBuildPlanDetail(updatedBuildPlanDetail);
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (propertyName === 'buildPlanName') {
            setBuildPlan(buildPlanOptions.find(bp => bp.buildPlanName === newValue));
            return;
        }
        setEditing(true);
        setBuildPlan(bp => ({ ...bp, [propertyName]: newValue }));
    };

    const columns = [
        {
            title: 'Part Number',
            id: 'partNumber',
            type: 'search',
            editable: true,
            search: searchParts,
            clearSearch: clearPartsSearch,
            searchResults: partsSearchResults,
            searchLoading: partsSearchLoading,
            searchTitle: 'Search Parts',
            selectSearchResult: selectPartSearchResult,
            required: true
        },
        {
            title: 'From Week',
            id: 'fromDate',
            type: 'linnWeek',
            editable: true,
            required: true
        },
        {
            title: 'To Week',
            id: 'toDate',
            type: 'linnWeek',
            editable: true
        },
        {
            title: 'Rule',
            id: 'ruleCode',
            type: 'dropdown',
            editable: true,
            options: buildPlanRuleOptions,
            required: true
        },
        {
            title: 'Weekly Build',
            id: 'quantity',
            type: 'number',
            editable: true
        },
        {
            title: 'Description',
            id: 'partDescription',
            type: 'text',
            editable: false
        }
    ];

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Fragment>
                        <Title text="Build Plans" />
                        <CreateButton createUrl="/production/maintenance/build-plans/create" />
                    </Fragment>
                </Grid>

                {itemErrors &&
                    itemErrors?.map(itemError => (
                        <Grid item xs={12}>
                            <ErrorCard errorMessage={`${itemError.item} ${itemError.statusText}`} />
                        </Grid>
                    ))}

                {buildPlansLoading ||
                buildPlanDetailsLoading ||
                buildPlanRulesLoading ||
                buildPlanDetailLoading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    <Fragment>
                        <SnackbarMessage
                            visible={buildPlanSnackbarVisible}
                            onClose={() => setBuildPlanSnackbarVisible(false)}
                            message="Save Successful"
                        />
                        <SnackbarMessage
                            visible={buildPlanDetailSnackbarVisible}
                            onClose={() => setBuildPlanDetailSnackbarVisible(false)}
                            message="Save Successful"
                        />
                        <Grid item xs={4}>
                            <Dropdown
                                fullWidth
                                items={buildPlanOptions}
                                label="Select Build Plan"
                                value={buildPlan.buildPlanName}
                                onChange={handleFieldChange}
                                propertyName="buildPlanName"
                                allowNoValue={false}
                                data-testid="buildPlanSelect"
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <InputField
                                value={buildPlan.description}
                                label="Description"
                                fullWidth
                                propertyName="description"
                                onChange={handleFieldChange}
                                maxLength={50}
                                disabled={!buildPlan.buildPlanName}
                            />
                        </Grid>
                        <Grid item xs={4} />
                        <Grid item xs={4}>
                            <InputField
                                value={buildPlan.dateCreated}
                                label="Date Created"
                                type="date"
                                fullWidth
                                disabled
                                propertyName="dateCreated"
                                onChange={handleFieldChange}
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <DatePicker
                                label="Date Invalid"
                                value={
                                    buildPlan.dateInvalid ? buildPlan.dateInvalid.toString() : null
                                }
                                onChange={value => {
                                    handleFieldChange('dateInvalid', value);
                                }}
                                disabled={!buildPlan.buildPlanName}
                            />
                        </Grid>
                        <Grid item xs={4} />
                        {buildPlan.buildPlanName && (
                            <Fragment>
                                <Grid item xs={12}>
                                    <SaveBackCancelButtons
                                        saveDisabled={
                                            !buildPlan.description &&
                                            !buildPlan.dateCreated &&
                                            !editing
                                        }
                                        saveClick={handleSaveClick}
                                        cancelClick={handleCancelClick}
                                        backClick={handleBackClick}
                                    />
                                </Grid>
                                <Grid item xs={12}>
                                    <Typography variant="subtitle1">Build Plan Details</Typography>
                                </Grid>
                                <Grid item xs={12}>
                                    <EditableTable
                                        columns={columns}
                                        rows={buildPlanDetailOptions.filter(
                                            bpd => bpd.buildPlanName === buildPlan.buildPlanName
                                        )}
                                        newRow={newRow}
                                        createRow={handleSaveBuildPlanDetail}
                                        saveRow={handleUpdateBuildPlanDetail}
                                    />
                                </Grid>
                            </Fragment>
                        )}
                    </Fragment>
                )}
            </Grid>
        </Page>
    );
}

BuildPlans.propTypes = {
    itemErrors: PropTypes.arrayOf(
        PropTypes.shape({
            status: PropTypes.number,
            statusText: PropTypes.string,
            details: PropTypes.shape({}),
            item: PropTypes.string
        })
    ),
    buildPlans: PropTypes.arrayOf(PropTypes.shape({})),
    buildPlansLoading: PropTypes.bool,
    buildPlanDetails: PropTypes.arrayOf(PropTypes.shape({})),
    buildPlanDetailsLoading: PropTypes.bool,
    buildPlanRules: PropTypes.arrayOf(PropTypes.shape({})),
    buildPlanRulesLoading: PropTypes.bool,
    partsSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    partsSearchLoading: PropTypes.bool,
    searchParts: PropTypes.func.isRequired,
    clearPartsSearch: PropTypes.func.isRequired,
    selectedBuildPlan: PropTypes.string,
    updateBuildPlan: PropTypes.func.isRequired,
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    updateBuildPlanDetail: PropTypes.func.isRequired,
    saveBuildPlanDetail: PropTypes.func.isRequired,
    buildPlanDetailLoading: PropTypes.bool,
    buildPlanDetail: PropTypes.shape({}),
    fetchBuildPlanDetails: PropTypes.func.isRequired,
    fetchBuildPlans: PropTypes.func.isRequired,
    fetchBuildPlanRules: PropTypes.func.isRequired,
    buildPlanSnackbarVisible: PropTypes.bool,
    setBuildPlanSnackbarVisible: PropTypes.func.isRequired,
    buildPlanDetailSnackbarVisible: PropTypes.bool,
    setBuildPlanDetailSnackbarVisible: PropTypes.func.isRequired
};

BuildPlans.defaultProps = {
    itemErrors: null,
    buildPlans: [],
    buildPlansLoading: false,
    buildPlanDetails: [],
    buildPlanDetailsLoading: false,
    buildPlanRules: [],
    buildPlanRulesLoading: false,
    partsSearchResults: null,
    partsSearchLoading: false,
    selectedBuildPlan: null,
    buildPlanDetailLoading: false,
    buildPlanDetail: null,
    buildPlanSnackbarVisible: false,
    buildPlanDetailSnackbarVisible: false
};
