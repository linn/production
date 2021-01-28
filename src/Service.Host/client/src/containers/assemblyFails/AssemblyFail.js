import { connect } from 'react-redux';
import { initialiseOnMount, getItemErrors } from '@linn-it/linn-form-components-library';
import AssemblyFail from '../../components/assemblyFails/AssemblyFail';
import assemblyFailActions from '../../actions/assemblyFailActions';
import assemblyFailSelectors from '../../selectors/assemblyFailSelectors';
import worksOrdersSelectors from '../../selectors/worksOrdersSelectors';
import worksOrdersActions from '../../actions/worksOrdersActions';
import productionTriggerLevelsSelectors from '../../selectors/productionTriggerLevelsSelectors';
import pcasRevisionsActions from '../../actions/pcasRevisionsActions';
import pcasRevisionsSelectors from '../../selectors/pcasRevisionsSelectors';
import citsActions from '../../actions/citsActions';
import citsSelectors from '../../selectors/citsSelectors';
import employeesActions from '../../actions/employeesActions';
import employeesSelectors from '../../selectors/employeesSelectors';
import assemblyFailFaultCodes from '../../actions/assemblyFailFaultCodesActions';
import assemblyFailFaultCodesSelectors from '../../selectors/assemblyFailFaultCodesSelectors';
import getProfile from '../../selectors/userSelectors';
import smtShiftsSelectors from '../../selectors/smtShiftsSelectors';
import smtShiftsActions from '../../actions/smtShiftsActions';
import assemblyFailFaultCodeSelectors from '../../selectors/assemblyFailFaultCodeSelectors';
import partsActions from '../../actions/partsActions';
import partsSelectors from '../../selectors/partsSelectors';

const mapStateToProps = (state, { match }) => ({
    item: assemblyFailSelectors.getItem(state),
    itemId: match.params.id,
    editStatus: assemblyFailSelectors.getEditStatus(state),
    loading: assemblyFailSelectors.getLoading(state),
    snackbarVisible: assemblyFailSelectors.getSnackbarVisible(state),
    itemErrors: getItemErrors(state),
    profile: getProfile(state),
    worksOrdersSearchResults: worksOrdersSelectors.getSearchItems(state).map(s => ({
        ...s,
        id: s.orderNumber.toString(),
        name: s.orderNumber?.toString(),
        description: s.partNumber
    })),
    worksOrdersSearchLoading: worksOrdersSelectors.getSearchLoading(state),
    clearWorksOrdersSearch: worksOrdersActions.clearSearch,
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
    partsSearchResults: partsSelectors
        .getSearchItems(state, 100)
        .map(s => ({ ...s, id: s.partNumber, name: s.partNumber })),
    partsSearchLoading: partsSelectors.getSearchLoading(state)
});

const initialise = ({ itemId }) => dispatch => {
    dispatch(assemblyFailActions.fetch(itemId));
    dispatch(employeesActions.fetch());
    dispatch(citsActions.fetch());
    dispatch(assemblyFailFaultCodes.fetch());
    dispatch(smtShiftsActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    updateItem: assemblyFailActions.update,
    setEditStatus: assemblyFailActions.setEditStatus,
    setSnackbarVisible: assemblyFailActions.setSnackbarVisible,
    searchWorksOrders: worksOrdersActions.search,
    fetchPcasRevisionsForBoardPart: pcasRevisionsActions.fetchByQueryString,
    clearWorksOrdersSearch: worksOrdersActions.clearSearch,
    searchParts: partsActions.search,
    clearPartsSearch: partsActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(AssemblyFail));
