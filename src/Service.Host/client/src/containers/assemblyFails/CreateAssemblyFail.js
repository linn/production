import { connect } from 'react-redux';
import { fetchErrorSelectors, initialiseOnMount } from '@linn-it/linn-form-components-library';
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

const mapStateToProps = state => ({
    item: {},
    editStatus: 'create',
    errorMessage: fetchErrorSelectors(state),
    loading: assemblyFailSelectors.getLoading(state),
    snackbarVisible: assemblyFailSelectors.getSnackbarVisible(state),
    profile: getProfile(state),
    worksOrders: worksOrdersSelectors.getItems(state),
    worksOrdersLoading: worksOrdersSelectors.getLoading(state),
    boardParts: productionTriggerLevelsSelectors.getItems(state),
    boardPartsLoading: productionTriggerLevelsSelectors.getLoading(state),
    pcasRevisions: pcasRevisionsSelectors.getItems(state),
    pcasRevisionsLoading: pcasRevisionsSelectors.getLoading(state),
    employees: employeesSelectors.getItems(state),
    cits: citsSelectors.getItems(state),
    faultCodes: assemblyFailFaultCodesSelectors.getItems(state)
});

const initialise = () => dispatch => {
    dispatch(assemblyFailActions.setEditStatus('create'));
    dispatch(productionTriggerLevelsActions.fetchByQueryString('searchTerm', 'PCAS'));
    dispatch(employeesActions.fetch());
    dispatch(citsActions.fetch());
    dispatch(assemblyFailFaultCodes.fetch())
};

const mapDispatchToProps = {
    initialise,
    addItem: assemblyFailActions.add,
    setEditStatus: assemblyFailActions.setEditStatus,
    setSnackbarVisible: assemblyFailActions.setSnackbarVisible,
    fetchItems: worksOrdersActions.fetchByQueryString,
    fetchPcasRevisionsForBoardPart: pcasRevisionsActions.fetchByQueryString,
    clearSearch: worksOrdersActions.reset
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(AssemblyFail));
