import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import TriggerLevel from '../../components/productionTriggerLevels/TriggerLevel';
import productionTriggerLevelActions from '../../actions/productionTriggerLevelActions';
import productionTriggerLevelSelectors from '../../selectors/productionTriggerLevelSelectors';
import partsActions from '../../actions/partsActions';
import partsSelectors from '../../selectors/partsSelectors';
import manufacturingRoutesActions from '../../actions/manufacturingRoutesActions';
import manufacturingRoutesSelectors from '../../selectors/manufacturingRoutesSelectors';
import citsActions from '../../actions/citsActions';
import citsSelectors from '../../selectors/citsSelectors';
import employeesActions from '../../actions/employeesActions';
import employeesSelectors from '../../selectors/employeesSelectors';
import workStationActions from '../../actions/workStationActions';
import workStationSelectors from '../../selectors/workStationSelectors';
import * as itemTypes from '../../itemTypes';
import productionTriggerLevelStateActions from '../../actions/productionTriggerLevelStateActions';
import triggersActions from '../../actions/productionTriggerLevelsSearchActions';
import triggersSelectors from '../../selectors/productionTriggerLevelsSearchSelectors';

const mapStateToProps = state => ({
    editStatus: productionTriggerLevelSelectors.getEditStatus(state),
    loading: productionTriggerLevelSelectors.getLoading(state),
    snackbarVisible: productionTriggerLevelSelectors.getSnackbarVisible(state),
    manufacturingRoutes: manufacturingRoutesSelectors.getItems(state),
    cits: citsSelectors.getItems(state),
    employees: employeesSelectors.getItems(state),
    itemErrors: getItemError(state, itemTypes.productionTriggerLevel.item),
    workStations: workStationSelectors.getItems(state),
    partsSearchResults: partsSelectors
        .getSearchItems(state)
        .map(s => ({ ...s, id: s.partNumber, name: s.partNumber })),
    partsSearchLoading: partsSelectors.getSearchLoading(state),
    applicationState: productionTriggerLevelSelectors.getApplicationState(state),
    appStateLoading: productionTriggerLevelSelectors.getApplicationStateLoading(state),
    triggersSearchLoading: triggersSelectors.getSearchLoading(state),
    triggerSearchResults: triggersSelectors
        .getSearchItems(state)
        .map(s => ({ ...s, id: s.partNumber, name: s.partNumber }))
});

const initialise = () => dispatch => {
    dispatch(productionTriggerLevelActions.setEditStatus('create'));
    dispatch(manufacturingRoutesActions.fetch(''));
    dispatch(citsActions.fetch());
    dispatch(employeesActions.fetch());
    dispatch(productionTriggerLevelStateActions.fetchState());
    dispatch(workStationActions.fetchByQueryString(''));
};

const mapDispatchToProps = {
    initialise,
    addItem: productionTriggerLevelActions.add,
    setEditStatus: productionTriggerLevelActions.setEditStatus,
    setSnackbarVisible: productionTriggerLevelActions.setSnackbarVisible,
    searchParts: partsActions.search,
    clearPartsSearch: partsActions.clearSearch,
    searchExistingTriggers: triggersActions.search,
    clearTriggersSearch: triggersActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(TriggerLevel));
