import { connect } from 'react-redux';
import { getItemErrors } from '@linn-it/linn-form-components-library';
import partActions from '../../actions/partActions';
import partSelectors from '../../selectors/partSelectors';
import partsActions from '../../actions/partsActions';
import partsSelectors from '../../selectors/partsSelectors';
import PartCadInfo from '../../components/partCadInfo/PartCadInfo';

const mapStateToProps = state => ({
    loading: partSelectors.getLoading(state),
    item: partSelectors.getItem(state),
    snackbarVisible: partSelectors.getSnackbarVisible(state),
    itemErrors: getItemErrors(state),
    editStatus: partSelectors.getEditStatus(state),
    partsSearchResults: partsSelectors
        .getSearchItems(state)
        .map(p => ({ ...p, id: p.partNumber, name: p.partNumber })),
    partsSearchLoading: partsSelectors.getSearchLoading(state)
});

const mapDispatchToProps = {
    searchParts: partsActions.search,
    clearPartsSearch: partsActions.clearSearch,
    updatePart: partActions.update,
    setSnackbarVisible: partActions.setSnackbarVisible,
    setEditStatus: partActions.setEditStatus
};

export default connect(mapStateToProps, mapDispatchToProps)(PartCadInfo);
