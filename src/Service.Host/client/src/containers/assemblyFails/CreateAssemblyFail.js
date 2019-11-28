import { connect } from 'react-redux';
import { getItemErrors, initialiseOnMount } from '@linn-it/linn-form-components-library';
import AssemblyFail from '../../components/assemblyFails/AssemblyFail';
import assemblyFailActions from '../../actions/assemblyFailActions';
import assemblyFailSelectors from '../../selectors/assemblyFailSelectors';
import getProfile from '../../selectors/userSelectors';
import worksOrdersSelectors from '../../selectors/worksOrdersSelectors';
import worksOrdersActions from '../../actions/worksOrdersActions';
import productionTriggerLevelsActions from '../../actions/productionTriggerLevelsActions';
import productionTriggerLevelsSelectors from '../../selectors/productionTriggerLevelsSelectors';
import pcasRevisionsActions from '../../actions/pcasRevisionsActions';
import pcasRevisionsSelectors from '../../selectors/pcasRevisionsSelectors';
import citsActions from '../../actions/citsActions';
import citsSelectors from '../../selectors/citsSelectors';
import employeesActions from '../../actions/employeesActions';
import employeesSelectors from '../../selectors/employeesSelectors';
import assemblyFailFaultCodes from '../../actions/assemblyFailFaultCodesActions';
import assemblyFailFaultCodesSelectors from '../../selectors/assemblyFailFaultCodesSelectors';
import smtShiftsSelectors from '../../selectors/smtShiftsSelectors';
import smtShiftsActions from '../../actions/smtShiftsActions';
import assemblyFailFaultCodeSelectors from '../../selectors/assemblyFailFaultCodeSelectors';

const mapStateToProps = state => ({
    item: {},
    editStatus: 'create',
    loading: assemblyFailSelectors.getLoading(state),
    snackbarVisible: assemblyFailSelectors.getSnackbarVisible(state),
    itemErrors: getItemErrors(state),
    profile: getProfile(state),
    worksOrdersSearchResults: worksOrdersSelectors
        .getSearchItems(state)
        .map(s => ({ ...s, id: s.orderNumber, name: s.orderNumber })),
    worksOrdersSearchLoading: worksOrdersSelectors.getSearchLoading(state),
    clearWorksOrdersSearch: worksOrdersActions.clearSearch,
    boardParts: productionTriggerLevelsSelectors.getItems(state),
    boardPartsLoading: productionTriggerLevelsSelectors.getLoading(state),
    pcasRevisions: pcasRevisionsSelectors.getItems(state),
    pcasRevisionsLoading: pcasRevisionsSelectors.getLoading(state),
    employees: employeesSelectors.getItems(state),
    employeesLoading: employeesSelectors.getLoading(state),
    cits: citsSelectors.getItems(state),
    citsLoading: citsSelectors.getLoading(state),
    smtShifts: smtShiftsSelectors.getItems(state),
    smtShiftsLoading: smtShiftsSelectors.getLoading(state),
    faultCodes: assemblyFailFaultCodesSelectors.getItems(state),
    faultCodesLoading: assemblyFailFaultCodeSelectors.getLoading(state),
    boardPartsSearchResults: productionTriggerLevelsSelectors
        .getSearchItems(state)
        .map(s => ({ ...s, id: s.partNumber, name: s.partNumber })),
    boardPartsSearchLoading: productionTriggerLevelsSelectors.getSearchLoading(state)
});

const initialise = () => dispatch => {
    dispatch(employeesActions.fetch());
    dispatch(citsActions.fetch());
    dispatch(assemblyFailFaultCodes.fetch());
    dispatch(smtShiftsActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    addItem: assemblyFailActions.add,
    setEditStatus: assemblyFailActions.setEditStatus,
    setSnackbarVisible: assemblyFailActions.setSnackbarVisible,
    searchWorksOrders: worksOrdersActions.search,
    fetchPcasRevisionsForBoardPart: pcasRevisionsActions.fetchByQueryString,
    clearWorksOrdersSearch: worksOrdersActions.clearSearch,
    searchBoardParts: productionTriggerLevelsActions.search,
    clearBoardPartsSearch: productionTriggerLevelsActions.clearSearch
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(AssemblyFail));
