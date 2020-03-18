import { connect } from 'react-redux';
import { getItemErrors } from '@linn-it/linn-form-components-library';
import partsActions from '../../actions/partsActions';
import partActions from '../../actions/partActions';
import partsSelectors from '../../selectors/partsSelectors';
import partSelectors from '../../selectors/partSelectors';
import MechPartSource from '../../components/mechPartSource/MechPartSource';

const mapStateToProps = state => ({
    partsSearchResults: partsSelectors
        .getSearchItems(state)
        .map(s => ({ ...s, id: s.partNumber, name: s.partNumber })),
    partsSearchLoading: partsSelectors.getSearchLoading(state),
    partsLoading: partsSelectors.getLoading(state),
    partLoading: partSelectors.getLoading(state),
    item: partSelectors.getItem(state),
    partSnackbarVisible: partSelectors.getSnackbarVisible(state),
    itemErrors: getItemErrors(state)
});

const mapDispatchToProps = {
    searchParts: partsActions.search,
    clearPartsSearch: partsActions.clearSearch,
    updatePart: partActions.update,
    setPartSnackbarVisible: partActions.setSnackbarVisible
};

export default connect(mapStateToProps, mapDispatchToProps)(MechPartSource);
