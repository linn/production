import React, { Fragment, useState, useEffect } from 'react';
import {
    Title,
    Loading,
    InputField,
    Dropdown,
    EditableTable,
    DatePicker,
    CreateButton,
    SaveBackCancelButtons
} from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import Page from '../../containers/Page';
// TODO get from shared
// import EditableTable from './EditableTable';

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
    selectedBuildPlanDetail,
    updateBuildPlanDetail,
    saveBuildPlanDetail,
    buildPlanDetailLoading,
    buildPlanDetail,
    fetchBuildPlanDetails,
    fetchBuildPlans,
    fetchBuildPlanRules
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
    }, [buildPlanDetail]);

    useEffect(() => {
        if (buildPlans) {
            setBuildPlanOptions(
                buildPlans.map(bp => ({
                    ...bp,
                    id: bp.buildPlanName,
                    displayText: bp.buildPlanName
                }))
            );
        }
    }, [buildPlans]);

    useEffect(() => {
        if (buildPlans && selectedBuildPlan) {
            setBuildPlan(buildPlanOptions.find(bp => bp.buildPlanName === selectedBuildPlan));
        }
    }, [buildPlans, selectedBuildPlan, buildPlanOptions]);

    useEffect(() => {
        if (selectedBuildPlanDetail && buildPlanDetailOptions.length) {
            const blop = buildPlanDetailOptions.find(bpd => bpd.id === selectedBuildPlanDetail);
            console.log(blop);
        }
    }, [selectedBuildPlanDetail, buildPlanDetailOptions]);

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

    // TODO add these in with refs to the detail updated
    const handleUpdateBuildPlanDetail = buildPlanDetail => {
        updateBuildPlanDetail(null, buildPlanDetail);
    };

    const handleSaveBuildPlanDetail = buildPlanDetail => {
        saveBuildPlanDetail(buildPlanDetail);
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
                {buildPlansLoading ||
                buildPlanDetailsLoading ||
                buildPlanRulesLoading ||
                buildPlanDetailLoading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    <Fragment>
                        <Grid item xs={4}>
                            <Dropdown
                                fullWidth
                                items={buildPlanOptions}
                                label="Select Build Plan"
                                value={buildPlan.buildPlanName}
                                onChange={handleFieldChange}
                                propertyName="buildPlanName"
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
