import { connect } from 'react-redux';
import {
    getItemError,
    getRequestErrors,
    initialiseOnMount
} from '@linn-it/linn-form-components-library';
import PartFail from '../../components/partFails/PartFail';
import partFailActions from '../../actions/partFailActions';
import partFailSelectors from '../../selectors/partFailSelectors';
import * as itemTypes from '../../itemTypes';
import partFailErrorTypesSelectors from '../../selectors/partFailErrorTypesSelectors';
import storagePlacesSelectors from '../../selectors/storagePlacesSelectors';
import storagePlacesActions from '../../actions/storagePlacesActions';
import partFailErrorTypesActions from '../../actions/partFailErrorTypesActions';
import partFailFaultCodesSelectors from '../../selectors/partFailFaultCodesSelectors';
import partFailFailFaultCodesActions from '../../actions/partFailFailFaultCodesActions';
import getProfile from '../../selectors/userSelectors';
import worksOrdersSelectors from '../../selectors/worksOrdersSelectors';

const mapStateToProps = (state, { match }) => ({
    item: partFailSelectors.getItem(state),
    errorTypes: partFailErrorTypesSelectors.getItems(state),
    faultCodes: partFailFaultCodesSelectors.getItems(state),
    storagePlaces: storagePlacesSelectors.getItems(state),
    itemId: match.params.id,
    editStatus: partFailSelectors.getEditStatus(state),
    loading: partFailSelectors.getLoading(state),
    snackbarVisible: partFailSelectors.getSnackbarVisible(state),
    itemError: getItemError(state, itemTypes.partFail.item),
    profile: getProfile(state),
    worksOrders: worksOrdersSelectors.getItems(state),
    worksOrdersLoading: worksOrdersSelectors.getLoading(state),
    requestErrors: getRequestErrors(state)
});

const initialise = ({ itemId }) => dispatch => {
    dispatch(partFailActions.fetch(itemId));
    dispatch(storagePlacesActions.fetch());
    dispatch(partFailErrorTypesActions.fetch());
    dispatch(partFailFailFaultCodesActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    updateItem: partFailActions.update,
    setEditStatus: partFailActions.setEditStatus,
    setSnackbarVisible: partFailActions.setSnackbarVisible
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(PartFail));
